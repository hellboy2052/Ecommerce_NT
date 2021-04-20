using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ServerSite.Controllers;
using ServerSite.Models;
using SharedVm;
using ServerSite.Data;

namespace XUniteTest
{
    public class CategoryTest:IDisposable
    {
        private SqliteConnection _connection;
        private ApplicationDbContext _dbContext;
        public CategoryTest()
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
            var category = new CategoryVm
            {
                Name="Watch"
                        
            };

            var controller = new CategoryController(_dbContext);
            var result = await controller.Post(category);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<CategoryVm>(createdAtActionResult.Value);
            Assert.Equal("Watch", returnValue.Name);
            
        }

        [Fact]
        public async Task GetCategory_Success()
        {
            _dbContext.Categories.Add(new Category
            {
                Name="watch"
            });
            await _dbContext.SaveChangesAsync();

            var controller = new CategoryController(_dbContext);
            var result = await controller.Get();

            var actionResult = Assert.IsType<ActionResult<IEnumerable<CategoryVm>>>(result);
            Assert.NotEmpty(actionResult.Value);
        }
    }
}
