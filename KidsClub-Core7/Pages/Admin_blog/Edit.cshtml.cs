using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KidsClub_Core7.Data;
using Microsoft.Extensions.Hosting;

namespace KidsClub.Pages.Admin_blog
{
    public class EditModel : PageModel
    {
        private readonly KidsClub_Core7.Data.KidsClubContext _context;
        private IHostEnvironment _environment;

        public EditModel(KidsClub_Core7.Data.KidsClubContext context, IHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public TblContent TblContent { get; set; }
        [BindProperty]
        public IFormFile Upload { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TblContent = await _context.TblContents
                .Include(t => t.Category)
                .Include(t => t.Parent).FirstOrDefaultAsync(m => m.Id == id);

            if (TblContent == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.TblCategories, "Id", "Title");
            ViewData["ParentId"] = new SelectList(_context.TblContents, "Id", "Title");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var file = Path.Combine(_environment.ContentRootPath, "wwwroot\\Pictures", Upload.FileName);

            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await Upload.CopyToAsync(fileStream);
            }
            TblContent.Picture = Upload.FileName;
            _context.Attach(TblContent).State = EntityState.Modified;

            try
            {
                TblContent.CategoryId = 4;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblContentExists(TblContent.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TblContentExists(int id)
        {
            return _context.TblContents.Any(e => e.Id == id);
        }
    }
}
