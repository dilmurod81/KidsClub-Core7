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
using KidsClub_Core7.Data;

namespace KidsClub.Pages.Admin_testimonials_member
{
    public class EditModel : PageModel
    {
        private readonly KidsClub.EFCorePowerTool.Context.KidsClubContext _context;

        public EditModel(KidsClub.EFCorePowerTool.Context.KidsClubContext context)
        {
            _context = context;
        }

        [BindProperty]
        public KidsClub.EFCorePowerTool.Entities.TblContent TblContents { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TblContents = await _context.TblContents
                .Include(t => t.Category)
                .Include(t => t.Parent).FirstOrDefaultAsync(m => m.Id == id);

            if (TblContents == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.TblCategory, "Id", "Id");
            ViewData["ParentId"] = new SelectList(_context.TblContents, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            TblContents.FromId = User.Identity.Name;
            TblContents.CategoryId = 1;
            TblContents.IsActive = true;
            TblContents.DateEntered = DateTime.Now;
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            await _context.Procedures.usp_TestimonialsAsync(TblContents.Id, TblContents.ParentId, TblContents.FromId, TblContents.ToId,
            TblContents.Title, TblContents.ShortDescription, TblContents.LongDescription, TblContents.Qty, TblContents.DisplayOrder, "Testimonials");
            _context.Attach(TblContents).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblContentExists(TblContents.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            if (TblContents.ParentId == null)
            {
                return RedirectToPage("./Index");

            }
            return RedirectToPage("./Details", new { id = TblContents.ParentId });

        }

        private bool TblContentExists(int id)
        {
            return _context.TblContents.Any(e => e.Id == id);
        }
    }
}
