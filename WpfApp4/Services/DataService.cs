using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WpfApp4.Models;

namespace WpfApp4.Services
{
    internal class DataService
    {
        private const string _DataSourceAddress = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

        private static async Task<Stream> GetDataStream()  // формирует поток данных байт
        {
            var client = new HttpClient();
            var response = await client.GetAsync(_DataSourceAddress, HttpCompletionOption.ResponseHeadersRead);
            return await response.Content.ReadAsStreamAsync();
        }

        private static IEnumerable<string> GetDataLines()  // разбивает поток на последовательность строк
        {

            using (var data_stream = GetDataStream().Result)
            {
                using (var data_reader = new StreamReader(data_stream))
                {
                    while (!data_reader.EndOfStream)
                    {
                        var line = data_reader.ReadLine();

                        if (string.IsNullOrWhiteSpace(line)) continue;
                        yield return line.Replace("Korea,", "Korea -");
                    }
                }
            }
        }


        private static DateTime[] GetDates() => GetDataLines()
          .First()      //Нам нужна только первая строка
          .Split(',')   //её нужно разбить по запятой
          .Skip(4)      //Пропустить первые 4 элемента, потому что в первых 4 элементах находиться: (название провинций, название страны,широта долгота) поэтому эти данные мы отбрасываем, а со всемиостальными работаем
          .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture)) //Все остальные данные мы расшифровываем с помощи класса time в режиме инвариантной культуре
          .ToArray(); // Результат превращаем в массив. Получаем массив времён



        private static IEnumerable<PlaceInfo> GetCountriesData()  //Этот метод позволяет извлечь информацию о каждой стране
        {
            var lines = GetDataLines()
                .Skip(1)
                .Select(line => line.Split(','));

            foreach (var row in lines)
            {
                var province = row[0].Trim();
                var country_name = row[1].Trim(' ', '"');
                var latitude = double.Parse(row[2]);
                var longitude = double.Parse(row[3]);
                var counts = row.Skip(4).Select(int.Parse).ToArray();


                yield return new PlaceInfo();

            }

        }


        public IEnumerable<CountryInfo> GetData()
        {

            var dates = GetDates();

            


            return Enumerable.Empty<CountryInfo>();
        }


       

    }
}
