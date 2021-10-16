using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace WpfApp4.Infrastructure.Converters.BaseConverter
{
    internal abstract class Converter : MarkupExtension, IValueConverter
    {

        public override object ProvideValue(IServiceProvider serviceProvider) => this;  // просто возвращаем сами себя, расширение разметки


        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotSupportedException("Обратное преобразование не поддерживается");

    }
}
