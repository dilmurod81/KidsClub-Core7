using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KidsClub.EFCorePowerTool.Context;
using KidsClub.EFCorePowerTool.Entities;
//using KidsClub_Core7.Data;
using Microsoft.AspNetCore.Identity;

namespace KidsClub.Pages.Admin_events_member
{
    public class CreateModel : PageModel
    {
        private readonly KidsClub.EFCorePowerTool.Context.KidsClubContext _context;
        private IHostEnvironment _environment;
        public CreateModel(KidsClub.EFCorePowerTool.Context.KidsClubContext context, IHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public IFormFile Upload { get; set; }
        public IActionResult OnGet()
        {
            ViewData["CategoryId"] = new SelectList(_context.Set<KidsClub.EFCorePowerTool.Entities.TblCategory>(), "Id", "Title");
            ViewData["ParentId"] = new SelectList(_context.TblContents, "Id", "Title");
            return Page();
        }

        [BindProperty]
        public KidsClub.EFCorePowerTool.Entities.TblContent TblContent { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                TblContent.IsActive = true;
                TblContent.CategoryId = 111;

                //if (!ModelState.IsValid)
                //{
                //    ViewData["CategoryId"] = new SelectList(_context.Set<KidsClub.EFCorePowerTool.Entities.TblCategory>(), "Id", "Title");
                //    ViewData["ParentId"] = new SelectList(_context.TblContents, "Id", "Title");
                //    return Page();
                //}
                var file = Path.Combine(_environment.ContentRootPath, "wwwroot\\Pictures", Upload.FileName);

                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await Upload.CopyToAsync(fileStream);
                }
                TblContent.Picture = Upload.FileName;

                await _context.Procedures.usp_Events_InsertAsync(TblContent.CategoryId, TblContent.FromId, TblContent.Title, TblContent.ShortDescription, TblContent.LongDescription, TblContent.Url, TblContent.Picture, TblContent.Icon, TblContent.Price, TblContent.DiscountPrice, TblContent.IsActive, TblContent.IsDefault, TblContent.IsArchived, TblContent.StartDate, TblContent.EndDate, TblContent.DateEntered);

                //_context.TblContents.Add(TblContent);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ViewData["CategoryId"] = new SelectList(_context.Set<KidsClub.EFCorePowerTool.Entities.TblCategory>(), "Id", "Title");
                ViewData["ParentId"] = new SelectList(_context.TblContents, "Id", "Title");
                return Page();
            }
        }
    }
}



