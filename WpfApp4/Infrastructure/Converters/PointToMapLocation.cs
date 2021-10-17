using System;
using System.Activities;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using WpfApp4.Infrastructure.Converters.BaseConverter;


namespace WpfApp4.Infrastructure.Converters
{
    [MarkupExtensionReturnType(typeof(PointToMapLocation))]
    [ValueConversion(typeof(Point), typeof(Location))]
    internal class PointToMapLocation /*: Converter*/
    {
        //public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    if (!(value is Point point)) return null;
        //    return new Location(point.X, point.Y);
        //}

        //public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    if (!(value is Location location)) return null;
        //    return new Point(location.Latitude, location.Latitude);
        //}

    }
}

// 4 урок. время: 3 35 20