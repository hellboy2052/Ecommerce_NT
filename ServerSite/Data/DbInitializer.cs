using ServerSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerSite.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Categories.Any())
            {
                return;
            }

            var categories = new Category[]
            {
                new Category{ Name = "Phone"},
                new Category{ Name = "Tablet"},
            };

            context.Categories.AddRange(categories);
            context.SaveChanges();

            var brands = new Brand[]
            {
                new Brand{ Name = "Apple" },
                new Brand{ Name = "Samsung"},
            };

            context.Brands.AddRange(brands);
            context.SaveChanges();

            var product = new Product[]
            {
                new Product{ Name = "IPhone",  CategoryId = 1, Description = "Iphone", Price = 20000,BrandId=1,Inventory=10},
                new Product{ Name = "Samsung",  CategoryId = 1, Description = "Samsung", Price = 25000,BrandId=2,Inventory=10},
            };

            context.Products.AddRange(product);
            context.SaveChanges();

            var images = new Image[]
            {
                new Image{ ProductId=1,ImagePath="/ip1"},
                new Image{ ProductId=2,ImagePath="/ss1"},
            };

            context.Images.AddRange(images);
            context.SaveChanges();




            var users = new User[]
            {
                new User{ Email="user1@gmail.com",fullName="this is user 1",PhoneNumber="00001",UserName="user1"},
                new User{ Email="user2@gmail.com",fullName="this is user 2",PhoneNumber="00002",UserName="user2"},
            };

            context.Users.AddRange(users);
            context.SaveChanges();

            var products = new List<Product>() {
                new Product(){ Name = "IPhone",  CategoryId = 1, Description = "Iphone", Price = 20000,BrandId=1,Inventory=10},
                new Product(){ Name = "Samsung",  CategoryId = 1, Description = "Samsung", Price = 25000,BrandId=2,Inventory=10}
            };

            var carts = new Cart()
            {
                UserId = "5cb251f5-4412-41c1-9934-30523309dcae",
                Products = products
            };

            context.Carts.AddRange(carts);
            context.SaveChanges();

            var rates = new Rate[]
            {
                new Rate{ ProductId=1,UserId="5cb251f5-4412-41c1-9934-30523309dcae",Comment="this good",totalStar=5,CreateDate=DateTime.Parse("01-01-2021")},
                new Rate{ ProductId=2,UserId="d6c5ce25-c81c-4e39-8ced-a60a1bb47bb5",Comment="this good too",totalStar=5,CreateDate=DateTime.Parse("02-02-2021")},
            };

            context.Rates.AddRange(rates);
            context.SaveChanges();


            var orders = new Order[]
        {
                new Order{ Products=products
                    ,UserId="5cb251f5-4412-41c1-9934-30523309dcae" },
                new Order{ Products=products,UserId="d6c5ce25-c81c-4e39-8ced-a60a1bb47bb5" }
        };

            context.Orders.AddRange(orders);
            context.SaveChanges();
            var orderDetails = new OrderDetail[]
           {
                new OrderDetail{ OrderId=1,Status=true,totalPrice=10000,UserPhone="00001",CraeteDate=DateTime.Parse("01-01-2021"),Address="add1" },
                new OrderDetail{ OrderId=2,Status=true,totalPrice=20000,UserPhone="00002",CraeteDate=DateTime.Parse("02-02-2022"),Address="add2" }
           };

            context.OrderDetails.AddRange(orderDetails);
            context.SaveChanges();
        }
    }
}
