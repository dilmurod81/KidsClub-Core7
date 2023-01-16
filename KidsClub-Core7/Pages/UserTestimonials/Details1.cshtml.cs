using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KidsClub.EFCorePowerTool.Entities;
//using KidsClub_Core7.Data;

namespace KidsClub.Pages.UserTestimonials
{
    public class Details1Model : PageModel
    {
        private readonly KidsClub.EFCorePowerTool.Context.KidsClubContext _context;

        public Details1Model(KidsClub.EFCorePowerTool.Context.KidsClubContext context)
        {
            _context = context;
        }
        [BindProperty]
        public TblContent TblCommentor { get; set; }// <-- Attendee (Single)
        public TblContent TblTestimony { get; set; }// <-- Testimony (Single)
        public List<TblContent>? TblComments { get; set; } = default!;// <-- Comments (Multi)

        public async Task<IActionResult> OnGetAsync(int? id)
        {

            if (id == null || _context.TblContents == null)
            {
                return NotFound();
            }
            await LoadEvents(id);

            return Page();
        }

        private async Task LoadEvents(int? id)
        {

            var Testimony = await _context.TblContents.FirstOrDefaultAsync(m => m.Id == id);
            var Commentor = await _context.TblContents.FirstOrDefaultAsync(m => m.ParentId == id && m.FromId == User.Identity.Name);
            var Comments = await _context.TblContents.Where(m => m.ParentId == id).ToListAsync();

            if (Commentor != null)
            {
                TblCommentor = Commentor;
            }
            if (Testimony != null)
            {
                TblTestimony = Testimony;
            }
            if (Comments != null)
            {
                TblComments = Comments.ToList();
            }
        }


        public async Task<IActionResult> OnPostAsync(int? id)
        {

            TblCommentor.IsActive = true;

            if (TblCommentor.IsActive == false)
            {
                //Delete
                TblCommentor.Title = null;
            }
            if (User.Identity.Name == null)
            {
                await LoadEvents(id);
                return Page();
            }
            TblCommentor.FromId = User.Identity.Name;
            TblCommentor.ParentId = id;

            await _context.Procedures.usp_TestimonialsAsync(TblCommentor.Id, TblCommentor.ParentId, TblCommentor.FromId, TblCommentor.ToId, TblCommentor.Title, TblCommentor.ShortDescription, TblCommentor.LongDescription, TblCommentor.Qty, TblCommentor.DisplayOrder, "Comments");


            //_context.TblContents.Add(TblContent);
            //await _context.SaveChangesAsync();

            await LoadEvents(id);

            return RedirectToPage("./Details", new { id = id });

            //return Page();
        }
        public async Task<IActionResult> OnGetDelete(int? attendeeId)
        {
            int? ParentId = 0;
            if (attendeeId != null)
            {

                var Attendee = await _context.TblContents.FirstOrDefaultAsync(m => m.Id == attendeeId);// && m.FromId == User.Identity.Name);
                if (Attendee != null)
                {
                    ParentId = Attendee.ParentId;
                    _context.TblContents.Remove(Attendee);
                    _context.SaveChanges();
                }
            }
            await LoadEvents(ParentId);
            return RedirectToPage("./Details", new { id = ParentId });
            //return Page();
        }
    }
}