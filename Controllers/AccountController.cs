﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using school.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace school.Views.Home
{
    public class AccountController :Controller
    {
        private readonly SchoolContext context;
        public AccountController(SchoolContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");

            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return RedirectToAction("Login");
            }
            ClaimsIdentity identity = null;
            User user = null;
            bool isAuthentication = false;
            if ((user = await context.User.SingleOrDefaultAsync(u => u.UserName == userName && u.Password == password)) != null){
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, "Admin")
                }, CookieAuthenticationDefaults.AuthenticationScheme);
                isAuthentication = true;
            }
            if (isAuthentication)
            {
                var principal = new ClaimsPrincipal(identity);

                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(string oldPassword, string newPassword)
        {
            if (string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword))
            {
                ModelState.AddModelError("", "لم يتم تغير الباسورد");
            }
            var user = await context.User.Where(u => u.UserName == User.Identity.Name).FirstOrDefaultAsync();
            if(string.Equals(user.Password,oldPassword)) { 
                user.Password = newPassword;
                context.Update(user);
                await context.SaveChangesAsync();
                return await Logout();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
