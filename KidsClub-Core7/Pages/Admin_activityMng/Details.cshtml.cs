using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KidsClub.Data;

namespace KidsClub.Pages.Admin_activityMng
{
    public class DetailsModel : PageModel
    {
        private readonly KidsClub.Data.ApplicationDbContext _context;

        public DetailsModel(KidsClub.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public TblActivityInventory TblActivityInventory { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TblActivityInventory = await _context.TblActivityInventory.FirstOrDefaultAsync(m => m.Id == id);

            if (TblActivityInventory == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
