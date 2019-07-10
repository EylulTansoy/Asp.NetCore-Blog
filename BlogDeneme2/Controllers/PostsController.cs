using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data.Context;
using Data.Model;
using BlogDeneme2.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using BlogDeneme2.Dto;

namespace BlogDeneme2.Controllers
{

    [AuthAttribute(RoleName = new string[] { "User", "Admin" })]
    public class PostsController : Controller
    {
        private readonly AppDbContext _context;

        public PostsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Posts
        [AuthAttribute(RoleName = new string[] { "Admin" })]
        public IActionResult Index()
        {
            var posts = _context.Posts.Include(p => p.Account).AsEnumerable();
            return View(posts);

        }

        // GET: Posts/Details/5
        [AuthAttribute(RoleName = new string[] { "Admin" })]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = _context.Posts
                .Include(p => p.Account)
                .FirstOrDefault(m => m.ID == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create 
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(CreatePostDto input)
        {
            if (ModelState.IsValid)
            {
                Post post = new Post();
                var userId = HttpContext.Session.GetInt32("UserId");
                var user = _context.Accounts.Include(x => x.Role).FirstOrDefault(x => x.ID == userId);
                if (user.Role.RoleName != "Admin")
                {
                    post.TaskType = false;
                }
                else
                {
                    post.TaskType = true;
                }
                post.AccountId = user.ID;
                post.CreatedDate = DateTime.Now;
                post.Title = input.Title;
                post.Description = input.Description;
                post.ImageUrl = input.ImageUrl;
               
                var tagSplit = input.Tags.Split(';');
                List<Tags> tags = new List<Tags>();
                foreach (var item in tagSplit)
                {
                    tags.Add(new Tags() { Name = item });
                }
                post.Tags = tags;
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(input);
        }

        // GET: Posts/Edit/5
        [AuthAttribute(RoleName = new string[] { "Admin" })]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = _context.Posts.Include(x=>x.Account).Include(x=>x.Tags).FirstOrDefault(x=>x.ID==id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AuthAttribute(RoleName = new string[] { "Admin" })]
        public IActionResult Edit(int id, Post post)
        {
            if (id != post.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                     _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            
            return View(post);
        }

        // GET: Posts/Delete/5
        [AuthAttribute(RoleName = new string[] { "Admin" })]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = _context.Posts
                .Include(p => p.Account)
                .FirstOrDefault(m => m.ID == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [AuthAttribute(RoleName = new string[] { "Admin" })]
        public IActionResult DeleteConfirmed(int id)
        {
            var tags = _context.Tags.Where(x => x.PostId == id);
            foreach (var item in tags)
            {
                _context.Tags.Remove(item);
            }
            _context.SaveChanges();
            var post = _context.Posts.Find(id);
            _context.Posts.Remove(post);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [AuthAttribute(RoleName = new string[] { "Admin" })]
        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.ID == id);
        }
    }
}
