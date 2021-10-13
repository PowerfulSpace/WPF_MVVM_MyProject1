using System.Collections.Generic;

namespace WpfApp4.Models
{
    internal class CountryInfo : PlaceInfo
    {
        public IEnumerable<PlaceInfo> ProvinceCounts { get; set; }
    }

}
