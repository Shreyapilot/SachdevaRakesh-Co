using Microsoft.AspNetCore.Mvc;
using SachdevaCo.Core.Model;
using SachdevaCo.Core.Models;
using SachdevaCo.Core.Model.ViewModels;
using SachdevaCo.Helpers;
using System;
using System.Linq;

public class RegisterController : Controller
{
    private readonly SachdevaCoDbContext _context;

    public RegisterController(SachdevaCoDbContext context)
    {
        _context = context;
    }

    // GET: /Register
    public IActionResult Index()
    {
        return View();
    }

    // POST: /Register
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        // Check if user already exists
        var existingUser = _context.Users.FirstOrDefault(u => u.Username == model.UserName || u.Email == model.Email);
        if (existingUser != null)
        {
            ViewData["ValidateMessage"] = "User already exists with this username or email.";
            return View(model);
        }

        // Create password hash and salt
        PasswordHelper.CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);

        // Create new user
        var user = new User
        {
            Username = model.UserName,
            Email = model.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            CreatedDate = DateTime.Now
        };

        _context.Users.Add(user);
        _context.SaveChanges();

        TempData["SuccessMessage"] = "Registration successful. Please login.";
        return RedirectToAction("Index", "Login");
    }
}
