using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KidsClub.EFCorePowerTool.Context;
using KidsClub.EFCorePowerTool.Entities;


namespace KidsClub.Pages.AttendingEvents
{
    public class Index2Model : PageModel
    {
        private readonly KidsClubContext _context;

        public Index2Model(KidsClubContext context)
        {
            _context = context;
        }

        public string? UserName { get; private set; }
        [BindProperty]
        public IList<TblContent> TblContent { get; set; }
        public IList<TblContent> TblAttendees { get; set; }

        public async Task OnGetAsync()
        {

            UserName = HttpContext.User.Identity.Name;

            TblAttendees = await _context.TblContents
              .Where(x => x.ParentId.Equals(98))
              .Include(t => t.Parent)
              .Include(t => t.Category).ToListAsync();

            TblContent = await _context.TblContents
                .Where(x => x.FromId.Equals(UserName))
                .Include(t => t.Parent)
                .Include(t => t.Category).ToListAsync();


        }
    }
}
