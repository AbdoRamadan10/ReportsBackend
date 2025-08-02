using ReportsBackend.Domain.Entities;
using ReportsBackend.Infrastracture.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsBackend.Infrastracture.Seeders
{
    class ScreenSeeds
    {

        public static void ScreenInitializer(ApplicationDbContext context)
        {



            if (context.Screens.Count() != 0)
            {
                return;
            }

            var screens = new Screen[]
            {
                new Screen{ Name="Dashboard",Description="لوحة القيادة",Path="/dashboard" },
                new Screen{ Name="Reports",Description="التقارير",Path="/reports" },
                new Screen{ Name="rolemanagment",Description="rolemanagment",Path="/roles" },
                new Screen{ Name="usermanagment",Description="User Managment Page",Path="/users" },

            };

            foreach (var screen in screens)
            {
                context.Screens.Add(screen);
            }


        }
    }

}
