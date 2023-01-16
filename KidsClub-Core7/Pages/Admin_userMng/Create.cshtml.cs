using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KidsClub_Core7.Data;
using Microsoft.Extensions.Hosting;

namespace KidsClub.Pages.Admin_userMng
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
            return Page();
        }

        [BindProperty]
        public AspNetUser AspNetUser { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("AspNetUser.Id");
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
            _context.AspNetUsers.Add(AspNetUser);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
