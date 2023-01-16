using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KidsClub_Core7.Data;

namespace KidsClub.Pages.Admin_roles
{
    public class DeleteModel : PageModel
    {
        private readonly KidsClub_Core7.Data.KidsClubContext _context;

        public DeleteModel(KidsClub_Core7.Data.KidsClubContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AspNetRole AspNetRole { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AspNetRole = await _context.AspNetRoles.FirstOrDefaultAsync(m => m.Id == id);

            if (AspNetRole == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AspNetRole = await _context.AspNetRoles.FindAsync(id);

            if (AspNetRole != null)
            {
                _context.AspNetRoles.Remove(AspNetRole);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
