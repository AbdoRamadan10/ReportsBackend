using ReportsBackend.Infrastracture.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Entities;

namespace Template.Infrastracture.Seeders
{
    public class ProductSeeds
    {
        public static void ProductInitializer(ApplicationDbContext context)
        {



            if (context.Products.Count() != 0)
            {
                return;
            }

            var products = new Product[]
            {
                new Product{ Name="Chips",Price=10,Description="Chips" },
                new Product{ Name="Tiger",Price=15,Description="Tiger" },
                new Product{ Name="Milk",Price=20,Description="Milk" },
                new Product{ Name="Water",Price=45,Description="Water" },
                new Product{ Name="Pepsi",Price=10,Description="Pepsi" },
                new Product{ Name="Cheese",Price=40,Description="Cheese" },
                new Product{ Name="Banana",Price=15,Description="Banana" },

            };

            foreach (var product in products)
            {
                context.Products.Add(product);
            }


        }
    }
}
