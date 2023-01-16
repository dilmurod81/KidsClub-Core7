using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KidsClub.EFCorePowerTool.Context;
using KidsClub.EFCorePowerTool.Entities;

namespace KidsClub.Pages.Testimonials
{
    public class IndexModel : PageModel
    {
        private readonly KidsClub.EFCorePowerTool.Context.KidsClubContext _context;

        public IndexModel(KidsClub.EFCorePowerTool.Context.KidsClubContext context)
        {
            _context = context;
        }

        public IList<TblContent> TblContent { get; set; }

        public async Task OnGetAsync(int? id)
        {
            TblContent = await _context.TblContents
                .Where(x => x.CategoryId.Equals(1) && x.ParentId.Equals(id))
                .Include(t => t.Category)
                .Include(t => t.Parent).ToListAsync();
        }
    }
}
