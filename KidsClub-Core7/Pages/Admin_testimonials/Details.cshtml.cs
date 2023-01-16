using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KidsClub.EFCorePowerTool.Context;
using KidsClub.EFCorePowerTool.Entities;

namespace KidsClub.Pages.Admin_testimonials
{
    public class DetailsModel : PageModel
    {
        private readonly KidsClub.EFCorePowerTool.Context.KidsClubContext _context;

        public DetailsModel(KidsClub.EFCorePowerTool.Context.KidsClubContext context)
        {
            _context = context;
        }

        public TblContent TblContent { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TblContent = await _context.TblContents
                .Include(t => t.Category)
                .Include(t => t.Parent).FirstOrDefaultAsync(m => m.Id == id);

            if (TblContent == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
