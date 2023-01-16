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

namespace KidsClub.Pages.Admin_userMng
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
        public AspNetUser AspNetUser { get; set; }
        [BindProperty]
        public IFormFile Upload { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AspNetUser = await _context.AspNetUsers.FirstOrDefaultAsync(m => m.Id == id);

            if (AspNetUser == null)
            {
                return NotFound();
            }
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
            AspNetUser.Picture = Upload.FileName;
            _context.Attach(AspNetUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AspNetUserExists(AspNetUser.Id))
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

        private bool AspNetUserExists(string id)
        {
            return _context.AspNetUsers.Any(e => e.Id == id);
        }
    }
}
