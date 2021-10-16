using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using WpfApp4.Infrastructure.Converters.BaseConverter;

namespace WpfApp4.Infrastructure.Converters
{
    class ToArray : MultiConverter
    {

        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var collection = new CompositeCollection();
            foreach (var value in values)
            {
                collection.Add(value);
            }
            return collection;
        }

        //public override object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => value as object[];


    }
}
