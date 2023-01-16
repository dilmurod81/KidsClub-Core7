using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KidsClub_Core7.Data;

namespace KidsClub.Pages.Admin_content
{
    public class CreateModel : PageModel
    {
        private readonly KidsClub_Core7.Data.KidsClubContext _context;
        private IHostEnvironment _environment;
        public CreateModel(KidsClub_Core7.Data.KidsClubContext context, IHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        [BindProperty]
        public IFormFile Upload { get; set; }
        public IActionResult OnGet()
        {
            ViewData["CategoryId"] = new SelectList(_context.Set<TblCategory>(), "Id", "Title");
            ViewData["ParentId"] = new SelectList(_context.TblContents, "Id", "Title");

            return Page();
        }

        [BindProperty]
        public TblContent TblContent { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                //if (TblContent.IsActive == null)
                //    TblContent.IsActive = false;
                if (TblContent.ParentId == 0)
                    TblContent.ParentId = null;
                if (TblContent.CategoryId == 0)
                    TblContent.CategoryId = null;
                //TblContent.DateEntered = DateTime.Now;
                //ModelState.Remove("DateEntered");

                if (!ModelState.IsValid)
                {
                    ViewData["CategoryId"] = new SelectList(_context.Set<TblCategory>(), "Id", "Title");
                    ViewData["ParentId"] = new SelectList(_context.TblContents, "Id", "Title");
                    return Page();
                }
                var file = Path.Combine(_environment.ContentRootPath, "wwwroot\\Pictures", Upload.FileName);

                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await Upload.CopyToAsync(fileStream);
                }
                TblContent.Picture = Upload.FileName;
                _context.TblContents.Add(TblContent);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ViewData["CategoryId"] = new SelectList(_context.Set<TblCategory>(), "Id", "Title");
                ViewData["ParentId"] = new SelectList(_context.TblContents, "Id", "Title");
                return Page();
            }
        }
    }
}
