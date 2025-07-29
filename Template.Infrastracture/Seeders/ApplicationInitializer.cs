using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportsBackend.Infrastracture.Data.Context;
using Template.Infrastracture.Seeders;

namespace ReportsBackend.Infrastracture.Seeders
{
    public class ApplicationInitializer
    {
        public static void Initializer(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();
            ScreenSeeds.ScreenInitializer(context);
            ProductSeeds.ProductInitializer(context);
            context.SaveChanges();
        }


    }
}