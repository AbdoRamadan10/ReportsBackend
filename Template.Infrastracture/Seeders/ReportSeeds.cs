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
                new Report{ Id=1,Name="Product", Description="Product",Query="select * from products",Path="products",PrivilegeId=1,HasDetail=false },
                new Report{ Id=2,Name="Student", Description="Student",Query="select * from dash_students",Path="students",PrivilegeId=1,HasDetail=true ,DetailId=3,DetailColumn="STUDENTID"},
                new Report{ Id=3,Name="StudentDetail", Description="StudentDetail",Query="select * from dash_studentdetails",Path="studentdetails",PrivilegeId=1 },
            };

            foreach (var report in reports)
            {
                context.Reports.Add(report);
            }

            context.SaveChanges();


        }
    }
}
