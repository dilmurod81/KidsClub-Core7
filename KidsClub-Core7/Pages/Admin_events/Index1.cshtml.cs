using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KidsClub.EFCorePowerTool.Context;
using KidsClub.EFCorePowerTool.Entities;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KidsClub.Pages.Admin_events
{
    public class Index1Model : PageModel
    {
        private readonly KidsClubContext _context;

        public Index1Model(KidsClubContext context)
        {
            _context = context;
        }

        public string? UserName { get; private set; }
        [BindProperty]
        public IList<TblContent> TblContent { get; set; }

        public async Task OnGetAsync(int? id)
        {
            UserName = HttpContext.User.Identity.Name;
            TblContent = await _context.TblContents
                //.Where(x => x.CategoryId.Equals(6) || x.ParentId.Equals(104))
                .Where(x => x.ParentId.Equals(id) || x.CategoryId.Equals(6))
                .Include(t => t.Parent)
                .Include(t => t.Category).ToListAsync();
        }
    }
}
