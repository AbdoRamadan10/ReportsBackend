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
    class RoleScreenSeeds
    {
        public static void RoleScreenInitializer(ApplicationDbContext context)
        {



            if (context.RoleScreens.Count() != 0)
            {
                return;
            }

            var roleScreens = new RoleScreen[]
            {
                new RoleScreen{RoleId=1,ScreenId=1},
                new RoleScreen{RoleId=5,ScreenId=1},
                new RoleScreen{RoleId=1,ScreenId=2},
                new RoleScreen{RoleId=5,ScreenId=2},
                new RoleScreen{RoleId=5,ScreenId=3},


            };

            foreach (var roleScreen in roleScreens)
            {
                context.RoleScreens.Add(roleScreen);
            }


        }
    }
}
