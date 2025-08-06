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

            var reportColumns = new List<ReportColumn>
                    {

                        new ReportColumn{Id=1, Field="ID", HeaderName="ID", Sortable=true, Filter="agNumberColumnFilter", Resizable=true, FloatingFilter=true, RowGroup=false, Hide=false, ReportId=2},
                        new ReportColumn{Id=2, Field="NAME", HeaderName="NAME", Sortable=true, Filter="agTextColumnFilter", Resizable=true, FloatingFilter=true, RowGroup=false, Hide=false, ReportId=2},
                        new ReportColumn{Id=3, Field="GRADE", HeaderName="GRADE", Sortable=true, Filter="agTextColumnFilter", Resizable=true, FloatingFilter=true, RowGroup=false, Hide=false, ReportId=2},
                        new ReportColumn{Id=4, Field="GENDER", HeaderName="GENDER", Sortable=true, Filter="agTextColumnFilter", Resizable=true, FloatingFilter=true, RowGroup=false, Hide=false, ReportId=2},
                        new ReportColumn{Id=5, Field="DATEOFBIRTH", HeaderName="DATEOFBIRTH", Sortable=true, Filter="agDateColumnFilter", Resizable=true, FloatingFilter=true, RowGroup=false, Hide=false, ReportId=2},
                        new ReportColumn{Id=6, Field="EMAIL", HeaderName="EMAIL", Sortable=true, Filter="agTextColumnFilter", Resizable=true, FloatingFilter=true, RowGroup=false, Hide=false, ReportId=2},
                        new ReportColumn{Id=7, Field="PHONENUMBER", HeaderName="PHONENUMBER", Sortable=true, Filter="agNumberColumnFilter", Resizable=true, FloatingFilter=true, RowGroup=false, Hide=false, ReportId=2},
                        new ReportColumn{Id=8, Field="ADDRESS", HeaderName="ADDRESS", Sortable=true, Filter="agTextColumnFilter", Resizable=true, FloatingFilter=true, RowGroup=false, Hide=false, ReportId=2},

                        new ReportColumn{Id=9, Field="ID", HeaderName="ID", Sortable=true, Filter="agNumberColumnFilter", Resizable=true, FloatingFilter=true, RowGroup=false, Hide=false, ReportId=3},
                        new ReportColumn{Id=10, Field="STUDENTID", HeaderName="STUDENTID", Sortable=true, Filter="agNumberColumnFilter", Resizable=true, FloatingFilter=true, RowGroup=false, Hide=false, ReportId=3},
                        new ReportColumn{Id=11, Field="EMERGENCYCONTACTNAME", HeaderName="EMERGENCYCONTACTNAME", Sortable=true, Filter="agTextColumnFilter", Resizable=true, FloatingFilter=true, RowGroup=false, Hide=false, ReportId=3},
                        new ReportColumn{Id=12, Field="EMERGENCYCONTACTPHONE", HeaderName="EMERGENCYCONTACTPHONE", Sortable=true, Filter="agTextColumnFilter", Resizable=true, FloatingFilter=true, RowGroup=false, Hide=false, ReportId=3},
                        new ReportColumn{Id=13, Field="HEALTHINFORMATION", HeaderName="HEALTHINFORMATION", Sortable=true, Filter="agTextColumnFilter", Resizable=true, FloatingFilter=true, RowGroup=false, Hide=false, ReportId=3},
                        new ReportColumn{Id=14, Field="ADDITIONALNOTES", HeaderName="ADDITIONALNOTES", Sortable=true, Filter="agTextColumnFilter", Resizable=true, FloatingFilter=true, RowGroup=false, Hide=false, ReportId=3},
                        new ReportColumn{Id=15, Field="ACADEMICADVISOR", HeaderName="ACADEMICADVISOR", Sortable=true, Filter="agTextColumnFilter", Resizable=true, FloatingFilter=true, RowGroup=false, Hide=false, ReportId=3},

                    };

            foreach (var reportColumn in reportColumns)
            {
                context.ReportColumns.Add(reportColumn);
            }

            context.SaveChanges();
        }
    }
}
