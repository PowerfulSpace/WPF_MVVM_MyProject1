using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp4.Infrastructure.Commands;
using WpfApp4.Models;
using WpfApp4.Services;
using WpfApp4.ViewModels.Base;

namespace WpfApp4.ViewModels
{
    internal class CountryStaticViewModel : ViewModel
    {
        private DataService _DataService;
        private MainWindowViewModel MainModel { get; }




        #region Команды

        public ICommand RefreshDataCommand { get; }

        private void OnRefreshDataCommandExecuted(object p)
        {
            Countries = _DataService.GetData();
        }

        #endregion





        public CountryStaticViewModel(MainWindowViewModel MainModel)
        {
            this.MainModel = MainModel;
            _DataService = new DataService();

            RefreshDataCommand = new LambdaCommand(OnRefreshDataCommandExecuted);
        }







        #region Свойства



        #region Countries Статистика по странам

        private IEnumerable<CountryInfo> _Countries;

        public IEnumerable<CountryInfo> Countries
        {
            get => _Countries; 
            private set => Set(ref _Countries, value);
        }

        #endregion


        #endregion


    }
}
