using System;
using System.Globalization;
using System.Windows.Markup;
using WpfApp4.Infrastructure.Converters.BaseConverter;

namespace WpfApp4.Infrastructure.Converters
{
    [MarkupExtensionReturnType(typeof(Add))]
    internal class Add : Converter
    {
        [ConstructorArgument("B")]
        public double B { get; set; } = 1;


        public Add() { }
        public Add(double K) => this.B = B;



        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;

            var x = System.Convert.ToDouble(value, culture);

            return x + B;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;

            var x = System.Convert.ToDouble(value, culture);

            return x - B;
        }


    }




}
