using Data.Context;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogDeneme2.ViewComponents
{
    [ViewComponent]
    public class LastTitleComponents : ViewComponent
    {
        private readonly AppDbContext _context;

        public LastTitleComponents(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await Task.Run(() =>
            {
                //--- Son 5 Title
                var lastFiveTitle = _context.Posts.OrderByDescending(x => x.ID).Take(5);

                return lastFiveTitle;
            }));
        }
    }
}




