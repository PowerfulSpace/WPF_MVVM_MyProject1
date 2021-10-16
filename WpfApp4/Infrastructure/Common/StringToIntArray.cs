using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace WpfApp4.Infrastructure.Common
{
    // формировать массив чисел на основе строки

    [MarkupExtensionReturnType(typeof(int[]))]   // обьясняем разметке, каким возвращаймым типом является
    internal class StringToIntArray : MarkupExtension
    {

        [ConstructorArgument("Str")]
        public string Str { get; set; }

        public StringToIntArray() { }
        public StringToIntArray(string Str) => this.Str = Str;


        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Str.Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries)   //Берём нашу строку, разбиваем её по ; и отрасываем все пустые элементы
                .DefaultIfEmpty()  // если ничего не будет, вернётся пустой массив
                .Select(int.Parse) // если что-то получится, то мы каждый элемент парсим в Int
                .ToArray();        // и превращаем в массив
        }

        public char Separator { get; set; } = ';';

    }
}
