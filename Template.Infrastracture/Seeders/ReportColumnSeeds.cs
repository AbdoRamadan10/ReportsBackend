using ReportsBackend.Domain.Entities;
using ReportsBackend.Infrastracture.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Infrastracture.Seeders
{
    class ReportColumnSeeds
    {
        public static void ReportColumnInitializer(ApplicationDbContext context)
        {



            if (context.ReportColumns.Count() != 0)
            {
                return;
            }

            var reportColumns = new ReportColumn[]
            {
                new  ReportColumn{Name="ID",DisplayName="ID",DataType="NUMBER",ReportId=5},
                new  ReportColumn{Name="NAME",DisplayName="NAME",DataType="NVARCHAR2",ReportId=5},
                new  ReportColumn{Name="DESCRIPTION",DisplayName="DESCRIPTION",DataType="NVARCHAR2",ReportId=5},
                new  ReportColumn{Name="PRICE",DisplayName="IPRICED",DataType="NUMBER",ReportId=5},
            };

            foreach (var reportColumn in reportColumns)
            {
                context.ReportColumns.Add(reportColumn);
            }


        }
    }
}
