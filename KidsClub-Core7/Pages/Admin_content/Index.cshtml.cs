using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KidsClub_Core7.Data;

namespace KidsClub.Pages.Admin_content
{
    public class IndexModel : PageModel
    {
        private readonly KidsClub_Core7.Data.KidsClubContext _context;

        public IndexModel(KidsClub_Core7.Data.KidsClubContext context)
        {
            _context = context;
        }

        public IList<TblContent> TblContent { get; set; }

        public async Task OnGetAsync()
        {
            string name = HttpContext.Request.Query["Category"];
            if (_context.TblContents != null)
            {
                TblContent = await _context.TblContents
                .Include(t => t.Category)
                .Include(t => t.Parent).ToListAsync();
            }
        }
    }
}
