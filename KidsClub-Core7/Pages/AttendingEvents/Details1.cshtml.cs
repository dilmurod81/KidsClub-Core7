using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KidsClub.EFCorePowerTool.Entities;

namespace KidsClub.Pages.AttendingEvents
{
    public class Details1Model : PageModel
    {
        private readonly KidsClub.EFCorePowerTool.Context.KidsClubContext _context;

        public Details1Model(KidsClub.EFCorePowerTool.Context.KidsClubContext context)
        {
            _context = context;
        }
        [BindProperty]
        public TblContent TblContent { get; set; }// <-- Attendee (Single)
        public TblContent TblEvent { get; set; }// <-- Event (Single)
        public List<TblContent>? TblAttendees { get; set; } = default!;// <-- Attendees (Multi)

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
            var Event = await _context.TblContents.FirstOrDefaultAsync(m => m.Id == id);
            var Attendee = await _context.TblContents.FirstOrDefaultAsync(m => m.ParentId == id && m.FromId == User.Identity.Name);
            var Attendees = await _context.TblContents.Where(m => m.ParentId == id).ToListAsync();

            if (Attendee != null)
            {
                TblContent = Attendee;
            }
            if (Event != null)
            {
                TblEvent = Event;
            }
            if (Attendees != null)
            {
                TblAttendees = Attendees.ToList();
            }
        }


        public async Task<IActionResult> OnPostAsync(int? id)
        {

            TblContent.IsActive = true;

            if (TblContent.IsActive == false)
            {
                //Delete
                TblContent.Title = null;
            }
            if (User.Identity.Name == null)
            {
                await LoadEvents(id);
                return Page();
            }
            TblContent.FromId = User.Identity.Name;
            await _context.Procedures.usp_EventAttendAsync(TblContent.Id, TblContent.FromId, id, /* <-- Parent ID */
                TblContent.Title, TblContent.ShortDescription, TblContent.Price, TblContent.StartDate, TblContent.EndDate, TblContent.Qty, TblContent.IsDefault, TblContent.IsArchived, "Attendees", TblContent.Icon);

            //TblContent.ParentId = id;
            //_context.TblContents.Add(TblContent);
            //await _context.SaveChangesAsync();

            await LoadEvents(id);

            return RedirectToPage("./Details1", new { id = id });

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
            return RedirectToPage("./Details1", new { id = ParentId });
            //return Page();
        }
    }
}