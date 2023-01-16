using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KidsClub.EFCorePowerTool.Context;
using KidsClub.EFCorePowerTool.Entities;

namespace KidsClub.Pages.Messages
{
    public class CreateModel : PageModel
    {
        private readonly KidsClub.EFCorePowerTool.Context.KidsClubContext _context;

        public CreateModel(KidsClub.EFCorePowerTool.Context.KidsClubContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.TblContents, "Id", "Id");
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
            //}
            TblContent.CategoryId = 21;
            _context.TblContents.Add(TblContent);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
