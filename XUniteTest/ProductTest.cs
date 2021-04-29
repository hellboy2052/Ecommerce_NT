using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ServerSite.Controllers;
using ServerSite.Data;
using SharedVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace XUniteTest
{
    public class ProductTest : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly ApplicationDbContext _dbContext;
        public ProductTest()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(_connection)
                .Options;
            _dbContext = new ApplicationDbContext(options);
            _dbContext.Database.EnsureCreated();
        }
        public void Dispose()
        {
            _connection.Close();
        }
        [Fact]
        public async Task PostCategory_Success()
        {

            var product = new ProductVm
            {
                Name = "pdtest1",
                AverageStar = 5,
                CategoryId = 1,
                Description = "demo des 1",

                Inventory = 100,
                Price = 100,

            };

            var controller = new ProductController(_dbContext);
            var result = await controller.CreateProduct(product);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<ProductVm>(createdAtActionResult.Value);
            Assert.Equal(product.Name, returnValue.Name);

        }
    }
}
