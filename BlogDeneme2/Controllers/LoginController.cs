using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogDeneme2.Dto;
using Data.Context;
using Data.Model;
using Microsoft.AspNetCore.Mvc;

namespace BlogDeneme2.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;
        public LoginController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            return View(new SignInDto());
        }
        [HttpPost]
        public IActionResult SignIn(SignInDto input)
        {
            var data = _context.Accounts.FirstOrDefault(x => x.UserName == input.UserName && x.Password == input.Password);
            if (data != null)
            {
                //"Giris Yapildi";
            }
            return View();
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View(new SignUpDto());
        }
        [HttpPost]
        public IActionResult SignUp(SignUpDto input)
        {
            _context.Accounts.Add(new Account()
            {
                EMail = input.EMail,
                Password = input.Password,
                UserName = input.UserName
            });
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}