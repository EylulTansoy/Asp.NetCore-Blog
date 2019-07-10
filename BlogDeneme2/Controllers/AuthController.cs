using BlogDeneme2.Dto;
using Data.Context;
using Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BlogDeneme2.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;
        public AuthController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //var tags = new List<Data.Model.Tags>()
            //{
            //    new Tags(){ Name = "DenemeTag1"},
            //    new Tags(){ Name = "DenemeTag1"},
            //    new Tags(){ Name = "DenemeTag1"}
            //};
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
            //include -> ilişkili olan tablodaki verileri getirmesi için kullanırız
            
            var data = _context.Accounts.Include(x=>x.Role).FirstOrDefault(x => x.UserName == input.UserName && x.Password == input.Password);
            if (data != null)
            {
                //Session tarayıcının belleğinde tutulur. 
                //Durum yönetiminde kullanılır.
                HttpContext.Session.SetString("UserName", data.UserName);
                HttpContext.Session.SetInt32("UserId", data.ID);
                HttpContext.Session.SetString("UserRole", data.Role.RoleName);
                
                return RedirectToAction("Index", "Home");

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
            var role = _context.Roles.SingleOrDefault(x => x.RoleName == "User");
            //--- DB'de user rolu yoksa
            if (role == null)
            {
                _context.Roles.Add(new Role()
                {
                    RoleName = "User"
                });
                _context.SaveChanges();
            }
            _context.Accounts.Add(new Account()
            {
                EMail = input.EMail,
                Password = input.Password,
                UserName = input.UserName,
                RoleId = _context.Roles.FirstOrDefault(x => x.RoleName == "User").ID
            });
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("UserRole");
            HttpContext.Session.Remove("UserId");
            return RedirectToAction("Index", "Home");
        }
    }
}