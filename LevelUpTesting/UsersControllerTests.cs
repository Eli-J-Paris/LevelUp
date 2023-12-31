﻿using LevelUp.DataAccess;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using LevelUp.Models;

namespace LevelUp.FeatureTests
{
    [Collection("Controller Tests")]
    public class UsersControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public UsersControllerTests(WebApplicationFactory<Program> factory)
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

        [Fact]
        public async Task Signup_ShowsNewUserForm()
        {
            var client = _factory.CreateClient();
            var context = GetDbContext();

            var response = await client.GetAsync("/users/signup");
            response.EnsureSuccessStatusCode();
            var html = await response.Content.ReadAsStringAsync();

            Assert.Contains("form", html);
            Assert.Contains("method=\"post\"", html);
            Assert.Contains("action=\"/users/create\"", html);
            Assert.Contains("label", html);
            Assert.Contains("input", html);
            Assert.Contains("button", html);
            Assert.Contains("Name", html);
            Assert.Contains("Password", html);
            Assert.Contains("Username", html);
        }

        [Fact]
        public async Task Create_CreatesNewUserAndRedirectsToProfilePage()
        {
            var client = _factory.CreateClient();
            var context = GetDbContext();
            var formData = new Dictionary<string, string>
            {
                {"Name", "Jane Doe"},
                {"Username", "j_doe" },
                {"Password", "abdefg" }
            };

            var response = await client.PostAsync("/users/create", new FormUrlEncodedContent(formData));
            response.EnsureSuccessStatusCode();
            var html = await response.Content.ReadAsStringAsync();

            Assert.Contains("Jane Doe", html);
        }

        [Fact]
        public async Task Test_UsersHaveLogInButton()
        {
            var context = GetDbContext();
            context.Users.Add(new User { Username = "Jim123", Name = "Jim", Password = "password123" });
            context.SaveChanges();

            var client = _factory.CreateClient();
            var response = await client.GetAsync("/users/login");
            var html = await response.Content.ReadAsStringAsync();

            Assert.Contains("Login", html);
        }

        [Fact]
        public async Task Test_UsersCanLogIn()
        {
            var context = GetDbContext();
            User user1 = new User { Username = "Jim123", Name = "Jim", Password = "password123" };
            user1.Password = user1.Encrypt(user1.Password);
            context.Users.Add(user1);
            context.SaveChanges();


            var formData = new Dictionary<string, string>
            {
                {"Username", "Jim123" },
                {"Password", "password123" }
            };

            var client = _factory.CreateClient();
            var response = await client.PostAsync("/users/login", new FormUrlEncodedContent(formData));

            response.EnsureSuccessStatusCode();
            var html = await response.Content.ReadAsStringAsync();

            Assert.Contains("{\"success\":true,\"redirectUrl\":\"/profile\"}", html);
        }
        [Fact]
        public async Task Test_LogIn_WrongPasswordLogInFails()
        {
            var context = GetDbContext();
            User user1 = new User { Username = "Jim123", Name = "Jim", Password = "password123" };
            user1.Password = user1.Encrypt(user1.Password);
            context.Users.Add(user1);
            context.SaveChanges();


            var formData = new Dictionary<string, string>
            {
                {"Username", "Jim123" },
                {"Password", "password321" }
            };

            var client = _factory.CreateClient();
            var response = await client.PostAsync("/users/login", new FormUrlEncodedContent(formData));

            response.EnsureSuccessStatusCode();
            var html = await response.Content.ReadAsStringAsync();

            Assert.Contains("{\"success\":false,\"message\":\"LogIn Failed\"}", html);
        }
    }
}
