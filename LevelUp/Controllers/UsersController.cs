﻿using LevelUp.DataAccess;
using LevelUp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using System.Security.Cryptography;
using System.Text;


namespace LevelUp.Controllers
{
    public class UsersController : Controller
    {
        private readonly LevelUpContext _context;
        public UsersController(LevelUpContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            user.Password = user.Encrypt(user.Password);
            _context.Users.Add(user);
            _context.SaveChanges();

            Response.Cookies.Append("activeUser", user.Id.ToString());
            Response.Cookies.Append("userAuth", user.Encrypt(user.Username));

            return Redirect("/profile");
        }

        [HttpGet("users/login")]
        public IActionResult LogIn(string username)
        {
            ViewBag.Username = username;

            return View();
        }

        [HttpPost("users/login")]
        public IActionResult LogIn(User userToLogin)
        {
            // Fetch the user with the given username from the database
            var user = _context.Users.FirstOrDefault(u => u.Username == userToLogin.Username);

            // If the user exists and the password is correct
            if (user != null && VerifyPassword(user, userToLogin))
            {
                return Json(new { success = true, redirectUrl = Url.Action("Profile", "Users") });
            }

            // If login fails, send a JSON response.
            return Json(new { success = false, message = "LogIn Failed" });
        }

        private bool VerifyPassword(User userStored, User userToLogin)
        {
            var hashedPasswordBase64 = userStored.Encrypt(userToLogin.Password);
            return userStored.Password == hashedPasswordBase64;
        }
        private User? GetActiveUser(HttpRequest request)
        {
            var userId = Convert.ToInt32(request.Cookies["activeUser"]);
            var userAuth = request.Cookies["userAuth"];

            User? user = _context.Users.FirstOrDefault(u => u.Id == userId);

            if (user != null)
            {
                if (user.Encrypt(user.Username) != userAuth)
                {
                    user = null;
                }
            }

            return user;
        }

        [Route("/profile")]
        public IActionResult Profile()
        {
            var user = GetActiveUser(Request);
            if (user == null) return Redirect("/");
            return View(user);
        }

        
    }
}
