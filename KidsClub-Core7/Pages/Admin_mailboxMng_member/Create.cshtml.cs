using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KidsClub_Core7.Data;
using Microsoft.EntityFrameworkCore;

namespace KidsClub.Pages.Admin_mailboxMng_member
{
    public class CreateModel : PageModel
    {
        private readonly KidsClub_Core7.Data.KidsClubContext _context;

        public CreateModel(KidsClub_Core7.Data.KidsClubContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["CategoryId"] = new SelectList(_context.TblCategories, "Id", "Title");
            ViewData["ParentId"] = new SelectList(_context.TblContents, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public TblContent TblContent { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}s
            TblContent.CategoryId = 45;
            TblContent.FromId = User.Identity.Name;
            TblContent.IsActive = true;
            _context.TblContents.Add(TblContent);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
