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
                //new  ReportColumn{Id=1,Field="ID",HeaderName="ID",Sortable=false,Filter=false,Resizable=false,FloatingFilter=false,RowGroup=false,Hide=false,ReportId=21},
                //new  ReportColumn{Id=2,Field="NAME",HeaderName="NAME",Sortable=false,Filter=false,Resizable=false,FloatingFilter=false,RowGroup=false,Hide=false,ReportId=21},
                //new  ReportColumn{Id=3,Field="GRADE",HeaderName="GRADE",Sortable=false,Filter=false,Resizable=false,FloatingFilter=false,RowGroup=false,Hide=false,ReportId=21},
                //new  ReportColumn{Id=4,Field="GENDER",HeaderName="GENDER",Sortable=false,Filter=false,Resizable=false,FloatingFilter=false,RowGroup=false,Hide=false,ReportId=21},
                //new  ReportColumn{Id=5,Field="DATEOFBIRTH",HeaderName="DATEOFBIRTH",Sortable=false,Filter=false,Resizable=false,FloatingFilter=false,RowGroup=false,Hide=false,ReportId=21},
                //new  ReportColumn{Id=6,Field="EMAIL",HeaderName="EMAIL",Sortable=false,Filter=false,Resizable=false,FloatingFilter=false,RowGroup=false,Hide=false,ReportId=21},
                //new  ReportColumn{Id=7,Field="PHONENUMBER",HeaderName="PHONENUMBER",Sortable=false,Filter=false,Resizable=false,FloatingFilter=false,RowGroup=false,Hide=false,ReportId=21},
                //new  ReportColumn{Id=8,Field="ADDRESS",HeaderName="ADDRESS",Sortable=false,Filter=false,Resizable=false,FloatingFilter=false,RowGroup=false,Hide=false,ReportId=21},

            };

            foreach (var reportColumn in reportColumns)
            {
                context.ReportColumns.Add(reportColumn);
            }


        }
    }
}
