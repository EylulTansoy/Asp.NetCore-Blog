using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Context;
using Data.Model;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace BlogDeneme2.ViewComponents
{
    [ViewComponent]
    public class TagViewComponents : ViewComponent
    {
        private readonly AppDbContext _context;

        public TagViewComponents(AppDbContext context)
        {
            _context = context;
        }
      
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await Task.Run(() =>
            {
                //--- Son 5 Tag
                var lastFiveTags = _context.Tags.OrderByDescending(x => x.ID).Take(5);

                return lastFiveTags;
            }));
        }
    }
}
