using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KidsClub_Core7.Data;
using Microsoft.EntityFrameworkCore;

namespace KidsClub.Pages.Admin_mailboxMng_member
{
    public class Create_ReplyModel : PageModel
    {
        private readonly KidsClub_Core7.Data.KidsClubContext _context;

        public Create_ReplyModel(KidsClub_Core7.Data.KidsClubContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()//int? id, string? FromId)
        {
            ViewData["CategoryId"] = new SelectList(_context.TblCategories, "Id", "Title");
            ViewData["ParentId"] = new SelectList(_context.TblContents, "Id", "Id");

            return Page();
        }

        [BindProperty]
        public TblContent TblContent { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD

        //public async Task<IActionResult> OnGetReply(int? id, string? FromId)
        //{
        //int? ParentId = 0;
        //if (FromId != null)
        //{

        //    var Attendee = await _context.TblContents.FirstOrDefaultAsync(m => m.Id == FromId);// && m.FromId == User.Identity.Name);
        //    if (Attendee != null)
        //    {
        //        ParentId = Attendee.ParentId;
        //        _context.TblContents.Remove(Attendee);
        //        _context.SaveChanges();
        //    }
        //}
        //await LoadEvents(ParentId);
        //return RedirectToPage("./Details", new { id = ParentId });
        //    return Page();
        //}

        public async Task<IActionResult> OnPostAsync(int? id, string FromId)
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            TblContent.CategoryId = 45;
            TblContent.ToId = FromId;
            TblContent.FromId = User.Identity.Name;
            TblContent.IsActive = true;
            _context.TblContents.Add(TblContent);
            await _context.SaveChangesAsync();



            return RedirectToPage("./Index");
        }

    }
}
