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
                new Product{ Id=1,Name="Chips",Price=10,Description="شيبسي",CategoryId=1,CategoryName="مأكولات",ExpirationDate=DateTime.Now.AddDays(200) },
                new Product{ Id=2,Name="Jacket",Price=1500,Description="جاكيت" ,CategoryId=2,CategoryName="ملابس",ExpirationDate=DateTime.Now.AddDays(200)},
                new Product{ Id=3,Name="Milk",Price=20,Description="حليب" ,CategoryId=1,CategoryName="مأكولات",ExpirationDate=DateTime.Now.AddDays(200)},
                new Product{ Id=4,Name="Samsung S23",Price=5000,Description="سامسونج اس 23" ,CategoryId=3,CategoryName="موبيلات",ExpirationDate=DateTime.Now.AddDays(200)},
                new Product{ Id=5,Name="Air conditioner",Price=10,Description="تكييف",CategoryId=4,CategoryName="أجهزة كهربائية",ExpirationDate=DateTime.Now.AddDays(200) },
                new Product{ Id=6, Name="Cheese",Price=40,Description="جبنة",CategoryId=1,CategoryName="مأكولات",ExpirationDate=DateTime.Now.AddDays(200) },
                new Product{ Id=7,Name="Iphone 15",Price=6500,Description="ايفون 15" ,CategoryId=3,CategoryName="موبيلات",ExpirationDate=DateTime.Now.AddDays(200)},
                new Product{ Id=8,Name="Iphone 11",Price=4500,Description="ايفون11" ,CategoryId=3,CategoryName="موبيلات",ExpirationDate=DateTime.Now.AddDays(200)},
                new Product{ Id=9,Name="Trouser",Price=500,Description="بنطلون" ,CategoryId=2,CategoryName="ملابس",ExpirationDate=DateTime.Now.AddDays(200)},
                new Product{ Id=10,Name="TV",Price=6000,Description="تليفزيون",CategoryId=4,CategoryName="أجهزة كهربائية",ExpirationDate=DateTime.Now.AddDays(200) },



            };

            foreach (var product in products)
            {
                context.Products.Add(product);
            }


        }
    }
}
