using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KidsClub_Core7.Data;

namespace KidsClub.Pages.Admin_games_member
{
    public class DeleteModel : PageModel
    {
        private readonly KidsClub_Core7.Data.KidsClubContext _context;

        public DeleteModel(KidsClub_Core7.Data.KidsClubContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TblContent = await _context.TblContents.FindAsync(id);

            if (TblContent != null)
            {
                _context.TblContents.Remove(TblContent);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
