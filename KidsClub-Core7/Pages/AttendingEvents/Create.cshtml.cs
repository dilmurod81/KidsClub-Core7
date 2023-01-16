using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KidsClub.EFCorePowerTool.Context;
using KidsClub.EFCorePowerTool.Entities;

namespace KidsClub.Pages.AttendingEvents
{
    public class CreateModel : PageModel
    {
        private readonly KidsClubContext _context;

        public CreateModel(KidsClubContext context)
        {
            _context = context;

        }
        public IActionResult OnGet(string? Title, string? Picture, DateTime? StartDate, DateTime? EndDate, string? ShortDescription)
        {
            ViewData["Picture"] = Picture;
            ViewData["Title"] = Title;
            ViewData["StartDate"] = StartDate;
            ViewData["EndDate"] = EndDate;
            ViewData["ShortDescription"] = ShortDescription;

            return Page();
        }

        [BindProperty]
        public TblContent TblContent { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int? id, string? Title, string? Picture)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    ViewData["CategoryId"] = new SelectList(_context.Set<TblCategory>(), "Id", "Title");
                    ViewData["ParentId"] = new SelectList(_context.TblContents, "Id", "Title");
                    return Page();
                }

                TblContent.FromId = HttpContext.User.Identity.Name;
                TblContent.ParentId = id;
                TblContent.Title = Title;
                TblContent.Picture = Picture;


                await _context.Procedures.usp_EventAttendAsync(TblContent.Id, TblContent.FromId, TblContent.ParentId, TblContent.Title, TblContent.ShortDescription, TblContent.Price, TblContent.StartDate, TblContent.EndDate, TblContent.Qty, TblContent.IsDefault, TblContent.IsArchived, "attendee", TblContent.Icon);

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



