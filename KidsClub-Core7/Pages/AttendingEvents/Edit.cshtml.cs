
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

namespace KidsClub.Pages.AttendingEvents
{
    public class EditModel : PageModel
    {
        private readonly KidsClubContext _context;


        public EditModel(KidsClubContext context)
        {
            _context = context;

        }

        [BindProperty]
        public TblContent TblContent { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, int? ParentId, string? Title)
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
            ViewData["ParentId"] = new SelectList(_context.TblContents, "Id", "Title");
            ViewData["CategoryId"] = new SelectList(_context.TblCategory, "Id", "Title");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, int? ParentId, string? Title)
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            TblContent.Title = Title;
            TblContent.ParentId = ParentId;
            TblContent.IsActive = true;
            TblContent.FromId = HttpContext.User.Identity.Name;

            await _context.Procedures.usp_EventAttendAsync(TblContent.Id, TblContent.FromId, TblContent.ParentId, TblContent.Title, TblContent.ShortDescription, TblContent.Price, TblContent.StartDate, TblContent.EndDate, TblContent.Qty, TblContent.IsDefault, TblContent.IsArchived, "attendee", TblContent.Icon);


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

            //return RedirectToPage("./Index2");
            return RedirectToPage("./Details", new { id = TblContent.ParentId });

        }

        private bool TblContentExists(int id)
        {
            return _context.TblContents.Any(e => e.Id == id);
        }
    }
}
