using BlogDeneme2.Dto;
using BlogDeneme2.Models;
using Data.Context;
using Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BlogDeneme2.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        //public IActionResult TagList()
        //{
        //    var tags = new List<Data.Model.Tags>()
        //    {
        //        new Tags(){ ID = 1},
        //        new Tags(){ ID = 2},
        //        new Tags(){ ID = 3}
        //    };
        //    return View();
        //}

        public IActionResult Index()
        {
            //var user = HttpContext.Session.Keys["UserId"];
            var post = _context.Posts.Include(x => x.Account).Include(x => x.Tags).Where(x => x.TaskType == true).Select(x =>
                new PostDto()
                {
                    Id = x.ID,
                    Title = x.Title,
                    ShortDescription = x.Description.Substring(50, x.Description.Length),
                    Image = x.ImageUrl,
                    CreateTime = x.CreatedDate.ToString("dd-MM-yyyy"),
                    YazarIsmi = x.Account.UserName,
                    Tags = x.Tags.Select(u => u.Name).Take(5).ToList()
                }).OrderByDescending(x => x.CreateTime).AsEnumerable();
            return View(post);
        }

        [HttpGet("PostDetail/{id}")]
        public IActionResult PostDetail(int id)
        {
            var post = _context.Posts.Include(x => x.Tags).Include(x => x.Account).FirstOrDefault(x => x.ID == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}
