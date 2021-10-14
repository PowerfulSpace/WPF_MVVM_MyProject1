using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp4.Services;
using WpfApp4.ViewModels.Base;

namespace WpfApp4.ViewModels
{
    internal class CountryStaticViewModel : ViewModel
    {
        private DataService _DataService;
        private MainWindowViewModel MainModel { get; }
        public CountryStaticViewModel(MainWindowViewModel MainModel)
        {
            this.MainModel = MainModel;

            _DataService = new DataService();

        }

    }
}
