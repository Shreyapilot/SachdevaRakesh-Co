using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SachdevaCo.Core.Model.IRepository;
using SachdevaCo.Core.Model.ViewModels;
using SachdevaCo.Core.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SachdevaCo.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginRepository _loginRepository;

        public LoginController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.ShowRegister = false;
            var model = new LoginRegisterViewModel
            {
                Login = new LoginViewModel(),
                Register = new RegisterViewModel()
            };
            return View("Index", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind(Prefix = "Login")] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _loginRepository.AuthenticateUser(model.Email, model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid credentials.");
                return View(model);
            }
            HttpContext.Session.SetString("UserRole", user.Role);
            HttpContext.Session.SetString("UserName", user.UserName);
            if (user.Role == "Admin")
                return RedirectToAction("Index", "AdminArticle");
            else
                return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.ShowRegister = true;

            var model = new LoginRegisterViewModel
            {
                Login = new LoginViewModel(),
                Register = new RegisterViewModel()
            };

            return View("Index", model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register([Bind(Prefix = "Register")] RegisterViewModel model)
        {
            {
                if (!ModelState.IsValid)
                {
                    foreach (var kvp in ModelState)
                    {
                        foreach (var error in kvp.Value.Errors)
                        {
                            Console.WriteLine($"KEY: {kvp.Key}  ERROR: {error.ErrorMessage}");
                        }
                    }

                    return View("Index", model);
                }

                CreatePasswordHash(model.Password, out byte[] hash, out byte[] salt);

                var user = new User
                {
                    Username = model.UserName,
                    Email = model.Email,
                    PasswordHash = hash,
                    PasswordSalt = salt,
                    CreatedDate = DateTime.Now,
                    Role = model.Role
                };

                _loginRepository.RegisterUser(user);

                TempData["SuccessMessage"] = "Your message has been sent successfully.";
                return RedirectToAction("Index");
            }
        }
        private bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(storedHash);
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
