using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp4.ViewModels.Base;

namespace WpfApp4.ViewModels
{

    #region Коментарии по данному классу
    // Когда мы делаем модели представления и нам необходимо их внутри разметки для привязки данных
    // то желательно, что бы эти модели реализовывали интерфейс INotifyPropertyChanged
    #endregion

    class DirectoryViewModel : ViewModel
    {
        private readonly DirectoryInfo _DirectoryInfo;


        public IEnumerable<DirectoryViewModel> SubDirectories
        {
            get
            {
                try
                {
                    var directories = _DirectoryInfo
                         .EnumerateDirectories()
                         .Select(dir_info => new DirectoryViewModel(dir_info.FullName));

                    return directories;
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e.ToString());
                }
                return Enumerable.Empty<DirectoryViewModel>();
            }
        }

        public IEnumerable<FileViewModel> Files
        {
            get
            {
                try
                {
                    var files = _DirectoryInfo
                    .EnumerateFiles()
                    .Select(file => new FileViewModel(file.FullName));

                    return files;
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e.ToString());
                }
                return Enumerable.Empty<FileViewModel>();
            }
        }

        public IEnumerable<object> DirectoryItems
        {
            get
            {
                try
                {
                    return SubDirectories.Cast<object>().Concat(Files);
                }
                catch (UnauthorizedAccessException e)
                {
                    Debug.WriteLine(e.ToString());
                }
                return Enumerable.Empty<object>();
            }
        }


        public string Name => _DirectoryInfo.Name;
        public string Path => _DirectoryInfo.FullName;
        public DateTime CreationTime => _DirectoryInfo.CreationTime;

        public DirectoryViewModel(string Path) => _DirectoryInfo = new DirectoryInfo(Path);

    }


    class FileViewModel : ViewModel
    {
        private readonly FileInfo _FileInfo;


        public string Name => _FileInfo.Name;
        public string Path => _FileInfo.FullName;
        public DateTime CreationTime => _FileInfo.CreationTime;


        public FileViewModel(string Path) => _FileInfo = new FileInfo(Path);

    }
}
