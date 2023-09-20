using LevelUp.DataAccess;
using LevelUp.FeatureTests;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelUpTesting
{
    public class TasksControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public TasksControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        private LevelUpContext GetDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<LevelUpContext>();
            optionsBuilder.UseInMemoryDatabase("TestDatabase");

            var context = new LevelUpContext(optionsBuilder.Options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }

        //[Fact]
        //public async Task TasksPageDisplaysTasksPage()
        //{
        //    var context = GetDbContext();
        //    var client = _factory.CreateClient();

        //    var response = await client.GetAsync("/users/tasks");
        //    response.EnsureSuccessStatusCode();
        //    var html = await response.Content.ReadAsStringAsync();

        //    Assert.Contains("Daily",html);
        //    Assert.Contains("weekly", html);
        //    Assert.Contains("Todos", html);

        //}
    }
}
