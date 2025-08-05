
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReportsBackend.Domain.AG_Grid
{
    public class FilterModel
    {
        public string? FilterType { get; set; }

        [JsonConverter(typeof(FilterValueConverter))]
        public object? Filter { get; set; }

        public string? Type { get; set; }

        // For date filters
        [JsonPropertyName("dateFrom")] // For System.Text.Json
        //[JsonProperty("dateFrom")]     // For Newtonsoft.Json
        public string? DateFrom { get; set; }

        [JsonPropertyName("dateTo")]   // For System.Text.Json
        //[JsonProperty("dateTo")]       // For Newtonsoft.Json
        public string? DateTo { get; set; }


    }
}
