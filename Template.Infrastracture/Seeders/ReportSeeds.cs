using ReportsBackend.Domain.Entities;
using ReportsBackend.Infrastracture.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Infrastracture.Seeders
{
    public class ReportSeeds
    {
        public static void ReportInitializer(ApplicationDbContext context)
        {



            if (context.Reports.Count() != 0)
            {
                return;
            }

            var reports = new Report[]
            {
                new Report{ Name="Product", Description="Product",Query="select * from products where id > :id",Path="\\products",PrivilegeId=4 },
            };

            foreach (var report in reports)
            {
                context.Reports.Add(report);
            }


        }
    }
}
