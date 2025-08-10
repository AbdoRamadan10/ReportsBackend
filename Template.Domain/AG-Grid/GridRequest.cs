using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Domain.AG_Grid
{
    public class GridRequest
    {
        public int StartRow { get; set; }
        public int EndRow { get; set; }
        public List<SortModel>? SortModel { get; set; }
        public Dictionary<string, FilterModel>? FilterModel { get; set; }
        //public List<ColumnVO>? RowGroupCols { get; set; }
        //public List<string>? GroupKeys { get; set; }
        public List<string>? SqlParameters { get; set; }

        public string? SearchTerm { get; set; }

    }
}
