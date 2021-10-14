using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using WpfApp4.Infrastructure.Commands;
using WpfApp4.Models.Decanat;
using WpfApp4.ViewModels.Base;

namespace WpfApp4.ViewModels
{
    class MainWindowViewModel : ViewModel
    {

        #region Вторичные ViewModel

        private readonly CountryStaticViewModel _CountryStaticViewModel;

        #endregion


        public ObservableCollection<Group> Groups {get;}

        public object[] CompositeCollection { get; }

        #region Тестирование виртуализации
        public IEnumerable<Student> TestStudents => Enumerable.Range(1, App.IsDesignMode ? 10 : 10_000)
           .Select(i => new Student
           {
               Name = $"Имя{i}",
               Surname = $"Фамилия{i}"
           });
        #endregion

        #region Команды

        #region CloseApplicationCommand

        public ICommand CloseApplicationCommand { get; }

        private void OnCloseApplicationCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        private bool CanCloseApplicationCommandExecute(object p) => true;

        #endregion


        #region CreateGroupCommand

        public ICommand CreateGroupCommand { get; }

        private bool CanCreateGroupCommandExecute(object p) => true;

        private void OnCreateGroupCommandExecuted(object p)
        {
            var group_max_index = Groups.Count + 1;
            var new_group = new Group
            {
                Name = $"Греппа {group_max_index}",
                Students = new ObservableCollection<Student>()
            };
            Groups.Add(new_group);
        }

        

        #endregion

        #region DeleteGroupCommand

        public ICommand DeleteGroupCommand { get; }

        private bool CanDeleteGroupCommandExecute(object p) => p is Group group && Groups.Contains(group);

        private void OnDeleteGroupCommandExecuted(object p)
        {
            if (!(p is Group group)) return;

            var group_index = Groups.IndexOf(group);
            Groups.Remove(group);
            if (group_index < Groups.Count)
            {
                SelectedGroup = Groups[group_index];
            }
        }

        #endregion

        #endregion


       




        public MainWindowViewModel()
        {

            _CountryStaticViewModel = new CountryStaticViewModel(this);

            CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCreateGroupCommandExecute);
            CreateGroupCommand = new LambdaCommand(OnCreateGroupCommandExecuted, CanCreateGroupCommandExecute);
            DeleteGroupCommand = new LambdaCommand(OnDeleteGroupCommandExecuted, CanDeleteGroupCommandExecute);



            #region Коллекция групп со студентами
            var studentIndex = 1;

            var students = Enumerable.Range(1, 10).Select(i => new Student
            {
                Name = $"Name {studentIndex}",
                Surname = $"Surname {studentIndex}",
                Patronymic = $"Patronymic {studentIndex++}",
                Birthday = DateTime.Now,
                Rating = 0

            });

            var groups = Enumerable.Range(1, 20).Select(i => new Group
            {
                Name = $"Группа {i}",
                Students = new ObservableCollection<Student>(students)
            });

            Groups = new ObservableCollection<Group>(groups);
            #endregion

            #region Работа с разными типами данных
            var data_list = new List<object>();

            data_list.Add("Hello World");
            data_list.Add(42);
            var group = Groups[1];
            data_list.Add(group);
            data_list.Add(group.Students[0]);

            CompositeCollection = data_list.ToArray();
            #endregion

            #region Фильтр групп со студентами

            _SelectedGroupStudents.Filter += OnStundetFiltered;

            #region работаем с фильтром

            #region Сортируем по имени, по убыванию
            //_SelectedGroupStudents.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Descending));
            #endregion

            #region Групируем студентов по имени
            //_SelectedGroupStudents.GroupDescriptions.Add(new PropertyGroupDescription("Name"));
            #endregion

            #endregion
            #endregion

        }




        #region Свойства



        #region выбранная директория Модель представлениея DirectoryViewModel

        public DirectoryViewModel DiskRootDir { get; } = new DirectoryViewModel("c:\\");



        private DirectoryViewModel _SelectedDirectory;

        public DirectoryViewModel SelectedDirectory
        {
            get => _SelectedDirectory;
            set => Set(ref _SelectedDirectory, value);
        }


        #endregion




        #region StudentFilterText

        private string _StudentFilterText;

        public string StudentFilterText
        {
            get => _StudentFilterText;
            set
            {
                if (!Set(ref _StudentFilterText, value)) return;
                _SelectedGroupStudents.View.Refresh();

            }
        }

        #endregion


        #region SelectedGroupStudents

        private readonly CollectionViewSource _SelectedGroupStudents = new CollectionViewSource();


        private void OnStundetFiltered(object sender, FilterEventArgs e)
        {
            if(!(e.Item is Student student))
            {
                e.Accepted = false;
                return;
            }

            var filter_text = _StudentFilterText;
            if (string.IsNullOrWhiteSpace(filter_text)) return;


            if (student.Name is null || student.Surname is null || student.Patronymic is null)
            {
                e.Accepted = false;
                return;
            }

            if (student.Name.IndexOf(filter_text, StringComparison.OrdinalIgnoreCase) != -1) return;
            if (student.Surname.IndexOf(filter_text, StringComparison.OrdinalIgnoreCase) != -1) return;
            if (student.Patronymic.IndexOf(filter_text, StringComparison.OrdinalIgnoreCase) != -1) return;

            #region Проверка через Contains. Но без чувствительности к регистру. т.к на этой версии не работает
            //if (student.Name.Contains(filter_text)) return;
            //if (student.Surname.Contains(filter_text)) return;
            //if (student.Patronymic.Contains(filter_text)) return;
            #endregion


            e.Accepted = false;

        }

        public ICollectionView SelectedGroupStudents => _SelectedGroupStudents?.View;


        #endregion


        #region Выбранная группа SelectedGroup

        private Group _SelectedGroup;

        public Group SelectedGroup
        {
            get => _SelectedGroup;
            set
            {
                if (!Set(ref _SelectedGroup, value)) return;

                _SelectedGroupStudents.Source = value?.Students;
                OnPropertyChanged(nameof(SelectedGroupStudents));
            }
        }

        #endregion


        #region SelectedPageIndex   int - номер выбранной вкладки

        private int _SelectedPageIndex = 1;

        public int SelectedPageIndex
        {
            get => _SelectedPageIndex;
            set => Set(ref _SelectedPageIndex, value);
        }

        #endregion

        #region SelectedCompositevalue

        private object _SelectedCompositevalue;

        public object SelectedCompositevalue
        {
            get => _SelectedCompositevalue;
            set => Set(ref _SelectedCompositevalue, value);
        }

        #endregion



        #region 1 урок. Заголовок окна, статус программы

        #region Заголовок окна

        private string _Title = "Анализ статистики";

        ///<summary>Заголовок окна</summary>

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion

        #region Статус программы

        private string _Status = "Готов!";

        ///<summary>Это статус</summary>

        public string Status
        {
            get => _Status;
            set => Set(ref _Status, value);
        }

        #endregion


        #endregion

        #endregion







    }
}
