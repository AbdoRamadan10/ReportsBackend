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
                new Screen{ Id=1,Name="Dashboard",Description="لوحة القيادة",Path="/dashboard" },
                new Screen{ Id=2,Name="Reports",Description="التقارير",Path="/reports" },
                new Screen{ Id=3,Name="rolemanagment",Description="rolemanagment",Path="/roles" },
                new Screen{ Id=4,Name="usermanagment",Description="User Managment Page",Path="/users" },

            };

            foreach (var screen in screens)
            {
                context.Screens.Add(screen);
            }

            context.SaveChanges();
        }
    }

}
