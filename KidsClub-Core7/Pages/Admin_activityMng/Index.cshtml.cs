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
    public class IndexModel : PageModel
    {
        private readonly KidsClub.Data.ApplicationDbContext _context;

        public IndexModel(KidsClub.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<TblActivityInventory> TblActivityInventory { get;set; }

        public async Task OnGetAsync()
        {
            TblActivityInventory = await _context.TblActivityInventory.ToListAsync();
        }
    }
}
