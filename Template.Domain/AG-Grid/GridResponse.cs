using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Domain.AG_Grid
{
    public class GridResponse<T>
    {
        public List<T> Rows { get; set; }
        public int LastRow { get; set; }
    }

}
