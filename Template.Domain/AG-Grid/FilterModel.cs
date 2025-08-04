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
    }
}
