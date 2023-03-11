using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KidsClub.EFCorePowerTool.Context;
using KidsClub.EFCorePowerTool.Entities;

namespace KidsClub.Pages.Admin_testimonials_member
{
    public class IndexModel : PageModel
    {
        private readonly KidsClub.EFCorePowerTool.Context.KidsClubContext _context;

        public IndexModel(KidsClub.EFCorePowerTool.Context.KidsClubContext context)
        {
            _context = context;
        }
        [BindProperty]
        public TblContent TblContents { get; set; }
        public IList<VwContentChildCountMurod> TblContent { get; set; }

        public async Task OnGetAsync(int? id)
        {
            TblContent = await _context.VwContentChildCountMurod
                .Where(x => x.CategoryId.Equals(112) && x.ParentId.Equals(id)).ToListAsync();
        }


        public async Task<IActionResult> OnPostAsync()
        {

            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            TblContents.FromId = User.Identity.Name;
            TblContents.CategoryId = 112;

            await _context.Procedures.usp_TestimonialsAsync(TblContents.Id, TblContents.ParentId, TblContents.FromId, TblContents.ToId,
                TblContents.Title, TblContents.ShortDescription, TblContents.LongDescription, TblContents.Qty, TblContents.DisplayOrder, "Testimonials");

            //_context.TblContents.Add(TblContents);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
