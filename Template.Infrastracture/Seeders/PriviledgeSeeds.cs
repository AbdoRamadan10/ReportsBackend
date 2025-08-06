using ReportsBackend.Domain.Entities;
using ReportsBackend.Infrastracture.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Entities;

namespace Template.Infrastracture.Seeders
{
    public class PrivilegeSeeds
    {
        public static void PrivilegeInitializer(ApplicationDbContext context)
        {



            if (context.Privileges.Count() != 0)
            {
                return;
            }

            var privileges = new Privilege[]
            {
                new Privilege{Id=1, Name="Export",Description="export",Path="/export" },


            };

            foreach (var privilege in privileges)
            {
                context.Privileges.Add(privilege);
            }

            context.SaveChanges();

        }
    }
}
