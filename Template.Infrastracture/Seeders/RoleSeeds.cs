
using ReportsBackend.Domain.Entities;
using ReportsBackend.Infrastracture.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Infrastracture.Seeders
{
    public class RoleSeeds
    {
        public static void RoleInitializer(ApplicationDbContext context)
        {



            if (context.Roles.Count() != 0)
            {
                return;
            }

            var roles = new Role[]
            {
                new Role{Name="ADMIN",Description="",Status=true },
                new Role{Name="USER",Description="",Status=true },
                new Role{Name="WH",Description="",Status=true },
                new Role{Name="WS",Description="",Status=true },
                new Role{Name="DEPT",Description="",Status=true },
            };

            foreach (var role in roles)
            {
                context.Roles.Add(role);
            }


        }
    }
}
