
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

        // For operator support (AND/OR)
        public string? Operator { get; set; }

        // For compound filters
        public List<FilterModel>? Conditions { get; set; }

        // For date filters
        [JsonPropertyName("dateFrom")]
        public string? DateFrom { get; set; }

        [JsonPropertyName("dateTo")]
        public string? DateTo { get; set; }
    }
}
