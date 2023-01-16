using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KidsClub.EFCorePowerTool.Context;
using KidsClub.EFCorePowerTool.Entities;

namespace KidsClub.Pages.AttendingEvents
{
    public class IndexModel : PageModel
    {
        private readonly KidsClubContext _context;

        public IndexModel(KidsClubContext context)
        {
            _context = context;
        }

        public string? UserName { get; private set; }

        public IList<TblContent> TblContent { get; set; }

        public async Task OnGetAsync()
        {
            UserName = HttpContext.User.Identity.Name;
            TblContent = await _context.TblContents
                .Where(x => x.CategoryId.Equals(6))// || x.FromId.Equals(UserName)
                .Include(t => t.Parent)
                .Include(t => t.Category).ToListAsync();
        }
    }
}
