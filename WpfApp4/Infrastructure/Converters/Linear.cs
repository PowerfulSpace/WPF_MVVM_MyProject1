using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;
using WpfApp4.Infrastructure.Converters.BaseConverter;

namespace WpfApp4.Infrastructure.Converters
{

    /// <summary>Реализация линейного преобразования f(x) = k*x + b</summary>
    [ValueConversion(typeof(double), typeof(double))]
    internal class Linear : Converter
    {

        [ConstructorArgument("K")]
        public double K { get; set; } = 1;

        [ConstructorArgument("B")]
        public double B { get; set; }


        public Linear() { }
        public Linear(double K) => this.K = K;
        public Linear(double K, double B) : this(K) => this.B = B;


        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;

            var x = System.Convert.ToDouble(value, culture);
            return K * x + B;

        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;

            var y = System.Convert.ToDouble(value, culture);
            return (y - B) / K;
        }
    }
}
