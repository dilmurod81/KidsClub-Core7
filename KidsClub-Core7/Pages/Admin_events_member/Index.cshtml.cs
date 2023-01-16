using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KidsClub.EFCorePowerTool.Context;
using KidsClub.EFCorePowerTool.Entities;
using NuGet.Versioning;

namespace KidsClub.Pages.Admin_events_member
{
    public class IndexModel : PageModel
    {
        private readonly KidsClub.EFCorePowerTool.Context.KidsClubContext _context;

        public IndexModel(KidsClub.EFCorePowerTool.Context.KidsClubContext context)
        {
            _context = context;
        }
        [BindProperty]
        public IList<VwContentChildCountMurod> TblContent { get; set; }

        public async Task OnGetAsync()
        {

            TblContent = await _context.VwContentChildCountMurod
                .Where(x => x.CategoryId.Equals(6)).ToListAsync();



        }
    }
}
