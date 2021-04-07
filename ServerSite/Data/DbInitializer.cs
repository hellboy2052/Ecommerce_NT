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
            //context.Database.EnsureCreated();

            if (context.Categories.Any())
            {
                return;
            }

            var categories = new Category[]
            {
                new Category{ Name = "Phone"},
                new Category{ Name = "Tablet"},
                new Category{ Name = "Laptop"},
            };

            context.Categories.AddRange(categories);
            context.SaveChanges();

            var brands = new Brand[]
            {

                new Brand{ Name = "Samsung"},
                new Brand{ Name = "Huwei"},
                new Brand{ Name = "Lenovo"},
                new Brand{ Name = "Acer"},
                new Brand{ Name = "Iphone"},
                new Brand{ Name = "Xiaomi"},


            };

            context.Brands.AddRange(brands);
            context.SaveChanges();

            var product = new Product[]
            {
                //Phone
            new Product
            {
            Name = "Điện thoại Samsung Galaxy A32",
                CategoryId = 1,
                Description = "Samsung Galaxy A32 4G là chiếc điện thoại thuộc phân khúc tầm trung nhưng sở " +
            "hữu nhiều ưu điểm vượt trội về màn hình lớn sắc nét, bộ bốn camera 64 MP cùng vi xử lý hiệu năng cao và được bán ra với mức giá vô cùng tốt.",
                Price = 6690000,
                BrandId = 1,
                Inventory = 10
            },
            new Product
            {
                Name = "Điện thoại iPhone 12 64GB",
                CategoryId = 1,
                Description = "Trong những tháng cuối năm 2020 Apple đã chính thức giới thiệu đến người dùng cũng" +
            " như iFan thế hệ iPhone 12 series mới với hàng loạt tính năng bức phá, thiết kế được lột xác hoàn toàn, hiệu năng đầy mạnh mẽ và một trong số đó chính là iPhone 12 64GB.",
                Price = 21990000,
                BrandId = 5,
                Inventory = 10
            },
                new Product
                {
                    Name = "Điện thoại Xiaomi Redmi Note 10 (6GB/128GB)",
                    CategoryId = 1,
                    Description = "Xiaomi đã trình làng chiếc điện thoại mang tên gọi là Xiaomi Redmi Note 10" +
                " với điểm nhấn chính là cụm 4 camera 48 MP, chip rồng Snapdragon 678 mạnh mẽ cùng nhiều nâng cấp như dung lượng pin 5.000 mAh và hỗ trợ sạc nhanh 33 W tiện lợi.",
                    Price = 21990000,
                    BrandId = 6,
                    Inventory = 10
                },
                //Tablet
            new Product
            {
                Name = "Máy tính bảng Huawei MatePad T10s ",
                CategoryId = 6,
                Description = "Chiếc máy tính bảng giá rẻ đáng mong chờ của Huawei, Huawei MatePad T10s cuối " +
            "cùng cũng đã chính thức ra mắt. Với vi xử lý 8 nhân mở ra một thế giới giải trí mượt mà, sống động từng khoảnh khắc với màn hình cực lớn, hé lộ một chiếc máy tính bảng" +
            " tốt trong tầm giá mà bất kỳ ai cũng đều yêu thích.",
                Price = 5290000,
                BrandId = 2,
                Inventory = 10
            },
                new Product{ Name = "Máy tính bảng Samsung Galaxy Tab S6 Lite",  CategoryId = 6, Description = "Sau thành công của Galaxy Tab S6, Samsung tiếp tục ra mắt Galaxy Tab S6 Lite" +
                " để chinh chiến ở phân khúc máy tính bảng giá rẻ hơn. Thiết bị vẫn hỗ trợ bút S Pen thần thánh, thiết kế kim loại cao cấp và màn hình, âm thanh giải trí đỉnh cao.",
                    Price = 9090000,BrandId=1,Inventory=10},
                new Product{ Name = "Máy tính bảng Lenovo Tab M10 ",  CategoryId = 6, Description = "Với bộ xử lý Intel Core i3 thế hệ thứ 10 tiên tiến cũng như các tùy chọn ổ cứng siêu nhanh, lưu trữ rộng lớn, Lenovo IdeaPad Flex 5 14IIL05 i3 chắc " +
                "chắn là một lựa chọn tuyệt vời để bạn sử dụng hàng ngày",
                    Price = 5190000,BrandId=3,Inventory=10},
                //Laptop
            new Product
            {
                Name = "Laptop Lenovo IdeaPad S340 14IIL i3 1005G1/8GB/512GB/Win10 ",
                CategoryId = 3,
                Description = "Lenovo IdeaPad S340 14IIL (81VV003VVN) sở hữu cấu hình khá, " +
            "hiệu năng ổn định và thiết kế tinh tế đẹp mắt. Đây sẽ là chiếc laptop văn phòng phù hợp với đối tượng sinh viên, dân văn phòng thường xuyên xử lý các tác vụ văn phòng, học tập" +
            " và chỉnh sửa hình ảnh cơ bản.",
                Price = 13690000,
                BrandId = 3,
                Inventory = 10
            },
                new Product{ Name = "Laptop Lenovo IdeaPad Flex 5 14IIL05 i3 1005G1/8GB/512GB/Win10 (81X1001TVN)",  CategoryId = 3, Description = "Với bộ xử lý Intel Core i3 thế hệ thứ 10 tiên tiến" +
                " cũng như các tùy chọn ổ cứng siêu nhanh, lưu trữ rộng lớn, Lenovo IdeaPad Flex 5 14IIL05 i3 chắc chắn là một lựa chọn tuyệt vời để bạn sử dụng hàng ngày.",
                    Price = 13490000,BrandId=3,Inventory=10},
                new Product{ Name = "Laptop Lenovo ThinkBook 15IIL i3 1005G1/4GB/512GB/Win10 (20SM00D9VN)",  CategoryId = 3, Description = "Laptop Lenovo ThinkBook 15IIL i3 (20SM00D9VN) sở hữu" +
                " thiết kế từ kim loại toát lên vẻ sang trọng, sắc sảo, cấu hình lí tưởng cho học tập, trình duyệt web khi trang bị bộ vi xử lý Intel thế hệ thứ 10 mới và ổ cứng SSD cực nhanh.",
                    Price = 11690000,BrandId=3,Inventory=10},
            };

            context.Products.AddRange(product);
            context.SaveChanges();

            var images = new Image[]
            {
                //phone
                new Image{ ProductId=1,ImagePath="/p1"},
                new Image{ ProductId=2,ImagePath="/p2"},
                new Image{ ProductId=3,ImagePath="/p3"},
                //tablet
                new Image{ ProductId=4,ImagePath="/tl1"},
                new Image{ ProductId=5,ImagePath="/tl2"},
                new Image{ ProductId=6,ImagePath="/tl3"},
                //laptop
                new Image{ ProductId=7,ImagePath="/lt1"},
                new Image{ ProductId=8,ImagePath="/lt2"},
                new Image{ ProductId=9,ImagePath="/lt3"},
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

            var rates = new Rate[]
            {
                    new Rate{ ProductId=1,UserId="1c3c47e9-4ce9-473e-b792-bcce1bcfc8ef",Comment="this good",totalStar=5,CreateDate=DateTime.Parse("01-01-2021")},
                    new Rate{ ProductId=2,UserId="33e9c277-ec7a-4185-8248-80eb82c7d230",Comment="this good too",totalStar=5,CreateDate=DateTime.Parse("02-02-2021")},
            };

            context.Rates.AddRange(rates);
            context.SaveChanges();

            var productOders1 = new List<Product>() {
                    new Product(){ Name = "Điện thoại Samsung Galaxy A32",CategoryId = 1,
                    Description = "Samsung Galaxy A32 4G là chiếc điện thoại thuộc phân khúc tầm trung nhưng sở " +
                    "hữu nhiều ưu điểm vượt trội về màn hình lớn sắc nét, bộ bốn camera 64 MP cùng vi xử lý hiệu năng cao và được bán ra với mức giá vô cùng tốt.",
                    Price = 6690000,
                    BrandId = 1,
                    Inventory = 10},
                    new Product(){
                    Name = "Máy tính bảng Huawei MatePad T10s ",
                    CategoryId = 6,
                    Description = "Chiếc máy tính bảng giá rẻ đáng mong chờ của Huawei, Huawei MatePad T10s cuối " +
                "cùng cũng đã chính thức ra mắt. Với vi xử lý 8 nhân mở ra một thế giới giải trí mượt mà, sống động từng khoảnh khắc với màn hình cực lớn, hé lộ một chiếc máy tính bảng" +
                " tốt trong tầm giá mà bất kỳ ai cũng đều yêu thích.",
                    Price = 5290000,
                    BrandId = 2,
                    Inventory = 10
                },
                };
            var productOders2 = new List<Product>() {
                    new Product(){
                    Name = "Laptop Lenovo IdeaPad S340 14IIL i3 1005G1/8GB/512GB/Win10 ",
                    CategoryId = 3,
                    Description = "Lenovo IdeaPad S340 14IIL (81VV003VVN) sở hữu cấu hình khá, " +
                "hiệu năng ổn định và thiết kế tinh tế đẹp mắt. Đây sẽ là chiếc laptop văn phòng phù hợp với đối tượng sinh viên, dân văn phòng thường xuyên xử lý các tác vụ văn phòng, học tập" +
                " và chỉnh sửa hình ảnh cơ bản.",
                    Price = 13690000,
                    BrandId = 3,
                    Inventory = 10
                },
                    new Product(){
                    Name = "Điện thoại iPhone 12 64GB",
                    CategoryId = 1,
                    Description = "Trong những tháng cuối năm 2020 Apple đã chính thức giới thiệu đến người dùng cũng" +
                " như iFan thế hệ iPhone 12 series mới với hàng loạt tính năng bức phá, thiết kế được lột xác hoàn toàn, hiệu năng đầy mạnh mẽ và một trong số đó chính là iPhone 12 64GB.",
                    Price = 21990000,
                    BrandId = 5,
                    Inventory = 10
                },
                    new Product
                    {
                        Name = "Điện thoại Xiaomi Redmi Note 10 (6GB/128GB)",
                        CategoryId = 1,
                        Description = "Xiaomi đã trình làng chiếc điện thoại mang tên gọi là Xiaomi Redmi Note 10" +
                    " với điểm nhấn chính là cụm 4 camera 48 MP, chip rồng Snapdragon 678 mạnh mẽ cùng nhiều nâng cấp như dung lượng pin 5.000 mAh và hỗ trợ sạc nhanh 33 W tiện lợi.",
                        Price = 21990000,
                        BrandId = 6,
                        Inventory = 10
                    },
                };
            var orders = new Order[]
        {
                    new Order{ Products=productOders1
                        ,UserId="1c3c47e9-4ce9-473e-b792-bcce1bcfc8ef" },
                    new Order{ Products=productOders2
                        ,UserId="33e9c277-ec7a-4185-8248-80eb82c7d230" }
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
