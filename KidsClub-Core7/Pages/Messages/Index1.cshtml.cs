using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KidsClub.EFCorePowerTool.Context;
using KidsClub.EFCorePowerTool.Entities;

namespace KidsClub.Pages.Messages
{
    public class Index1Model : PageModel
    {
        private readonly KidsClub.EFCorePowerTool.Context.KidsClubContext _context;

        public Index1Model(KidsClub.EFCorePowerTool.Context.KidsClubContext context)
        {
            _context = context;
        }

        public IList<TblContent> TblContent { get; set; }

        public async Task OnGetAsync()
        {
            TblContent = await _context.TblContents
                .Where(x => x.ToId == User.Identity.Name)
                .Include(t => t.Category)
                .Include(t => t.Parent).ToListAsync();
        }
    }
}