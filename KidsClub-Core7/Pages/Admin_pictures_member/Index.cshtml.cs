using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KidsClub.EFCorePowerTool.Context;
using Microsoft.EntityFrameworkCore;
//using KidsClub_Core7.Data;
using KidsClub.EFCorePowerTool.Entities;

namespace KidsClub.Pages.Admin_pictures_member
{
    public class IndexModel : PageModel
    {
        private readonly KidsClub.EFCorePowerTool.Context.KidsClubContext _context;

        public IndexModel(KidsClub.EFCorePowerTool.Context.KidsClubContext context)
        {
            _context = context;
        }

        public IList<VwContentChildCountMurod> TblContent { get; set; }

        public async Task OnGetAsync()
        {
            TblContent = await _context.VwContentChildCountMurod
                .Where(x => x.CategoryId.Equals(3) && x.ParentId == null).ToListAsync();
        }
    }
}
