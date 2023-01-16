using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KidsClub_Core7.Data;

namespace KidsClub.Pages.Admin_profile
{
    public class DetailsModel : PageModel
    {
        private readonly KidsClub_Core7.Data.KidsClubContext _context;

        public DetailsModel(KidsClub_Core7.Data.KidsClubContext context)
        {
            _context = context;
        }

        public AspNetUser AspNetUser { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AspNetUser = await _context.AspNetUsers.FirstOrDefaultAsync(m => m.Id == id);

            if (AspNetUser == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
