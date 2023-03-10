using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KidsClub_Core7.Data;

namespace KidsClub.Pages.Admin_activityMng
{
    public class CreateModel : PageModel
    {
        private readonly KidsClub_Core7.Data.ApplicationDbContext _context;

        public CreateModel(KidsClub_Core7.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public TblActivityInventory TblActivityInventory { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.TblActivityInventory.Add(TblActivityInventory);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
