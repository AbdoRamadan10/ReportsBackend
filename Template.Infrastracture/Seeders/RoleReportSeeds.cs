using ReportsBackend.Domain.Entities;
using ReportsBackend.Infrastracture.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Entities;

namespace ReportsBackend.Infrastracture.Seeders
{
    class RoleReportSeeds
    {
        public static void RoleReportInitializer(ApplicationDbContext context)
        {



            if (context.RoleReports.Count() != 0)
            {
                return;
            }

            var roleReports = new RoleReport[]
            {
                new RoleReport{RoleId=1,ReportId=5},
                new RoleReport{RoleId=5,ReportId=5},


            };

            foreach (var roleReport in roleReports)
            {
                context.RoleReports.Add(roleReport);
            }


        }
    }
}
