
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KidsClub.EFCorePowerTool.Context;
using KidsClub.EFCorePowerTool.Entities;

namespace KidsClub.Pages.Admin_events
{
    public class EditModel : PageModel
    {
        private readonly KidsClub.EFCorePowerTool.Context.KidsClubContext _context;
        private IHostEnvironment _environment;

        public EditModel(KidsClub.EFCorePowerTool.Context.KidsClubContext context, IHostEnvironment environment)
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
            ViewData["ParentId"] = new SelectList(_context.TblContents, "Id", "Id");
            ViewData["CategoryId"] = new SelectList(_context.TblCategory, "Id", "Title");

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

            TblContent.CategoryId = 111;

            await _context.Procedures.usp_Events_UpdateAsync(TblContent.Id, TblContent.ParentId, TblContent.CategoryId, TblContent.FromId, TblContent.Title, TblContent.ShortDescription, TblContent.LongDescription, TblContent.Url, TblContent.Picture, TblContent.Icon, TblContent.Price, TblContent.DiscountPrice, TblContent.IsActive, TblContent.IsDefault, TblContent.IsArchived, TblContent.StartDate, TblContent.EndDate, TblContent.DateEntered);

            
            _context.Attach(TblContent).State = EntityState.Modified;

            try
            {
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
