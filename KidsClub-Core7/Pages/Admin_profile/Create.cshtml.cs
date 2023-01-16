using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KidsClub_Core7.Data;

namespace KidsClub.Pages.Admin_profile
{
    public class CreateModel : PageModel
    {
        private readonly KidsClub_Core7.Data.KidsClubContext _context;

        public CreateModel(KidsClub_Core7.Data.KidsClubContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AspNetUser AspNetUser { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.AspNetUsers.Add(AspNetUser);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
