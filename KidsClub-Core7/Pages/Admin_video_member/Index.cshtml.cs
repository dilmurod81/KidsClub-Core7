using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KidsClub.EFCorePowerTool.Context;
using KidsClub.EFCorePowerTool.Entities;

namespace KidsClub.Pages.Admin_video_member
{
    public class IndexModel : PageModel
    {
        private readonly KidsClubContext _context;

        public IndexModel(KidsClubContext context)
        {
            _context = context;
        }

        public IList<VwContentChildCountMurod> TblContent { get; set; }

        public async Task OnGetAsync()
        {
            TblContent = await _context.VwContentChildCountMurod.Where(x => x.CategoryId.Equals(108)).ToListAsync();
        }
    }
}
