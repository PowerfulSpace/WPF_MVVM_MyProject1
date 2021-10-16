using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using WpfApp4.Infrastructure.Commands;
using WpfApp4.Models;
using WpfApp4.Services;
using WpfApp4.ViewModels.Base;

namespace WpfApp4.ViewModels
{
    [MarkupExtensionReturnType(typeof(CountryStaticViewModel))]
    internal class CountryStaticViewModel : ViewModel
    {
        private readonly DataService _DataService;
        private MainWindowViewModel MainModel { get; }




        #region Команды

        public ICommand RefreshDataCommand { get; }

        private void OnRefreshDataCommandExecuted(object p)
        {
            Countries = _DataService.GetData();
        }

        private bool CanRefreshDataCommandExecute(object p) => true;

        #endregion



        public CountryStaticViewModel() : this(null)
        {
            if (!App.IsDesignMode)
            {
                throw new InvalidOperationException("Вызов конструктора, не преднозначенного для использования в обычном режиме");  
            }

            _Countries = Enumerable.Range(1, 10).Select(i => new CountryInfo
            {
                Name = $"Country {i}",
                ProvinceCounts = Enumerable.Range(1, 10).Select(j => new PlaceInfo
                {
                    Name = $"Province {i}",
                    Location = new Point(i, j),
                    Counts = Enumerable.Range(1, 10).Select(k => new ConfirmedCount
                    {
                        Date = DateTime.Now.Subtract(TimeSpan.FromDays(100 - k)),
                        Count = k
                    }).ToArray()
                }).ToArray()
            }).ToArray();
        }

        public CountryStaticViewModel(MainWindowViewModel MainModel)
        {
            this.MainModel = MainModel;
            _DataService = new DataService();

            RefreshDataCommand = new LambdaCommand(OnRefreshDataCommandExecuted, CanRefreshDataCommandExecute);
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




        #region SelectedCountry Выбранная страна

        private IEnumerable<CountryInfo> _SelectedCountry;

        public IEnumerable<CountryInfo> SelectedCountry
        {
            get => _SelectedCountry;
            set => Set(ref _SelectedCountry, value);
        }

        #endregion






        #endregion


    }
}
