using ReportsBackend.Domain.Entities;
using ReportsBackend.Infrastracture.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Infrastracture.Seeders
{
    class ReportParameterSeeds
    {
        public static void ReportParameterInitializer(ApplicationDbContext context)
        {



            if (context.ReportParameters.Count() != 0)
            {
                return;
            }

            var reportParameters = new ReportParameter[]
            {
                new  ReportParameter{Id=1,Name="CATEGORYID",DisplayName="CATEGORYID",DataType="NUMBER",ParameterType="NUMBER",IsRequired=false,DefaultValue="1",QueryForDropdown="A",Sort=1,ReportId=1},
                new  ReportParameter{Id=2,Name="PRICE",DisplayName="PRICE",DataType="NUMBER",ParameterType="NUMBER",IsRequired=false,DefaultValue="1",QueryForDropdown="A",Sort=2,ReportId=1},
            };

            foreach (var reportParameter in reportParameters)
            {
                context.ReportParameters.Add(reportParameter);
            }
            context.SaveChanges();

        }
    }
}
