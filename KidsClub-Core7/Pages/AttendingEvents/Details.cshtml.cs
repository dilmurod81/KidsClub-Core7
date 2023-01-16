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
    public class DetailsModel : PageModel
    {
        private readonly KidsClub.EFCorePowerTool.Context.KidsClubContext _context;

        public DetailsModel(KidsClub.EFCorePowerTool.Context.KidsClubContext context)
        {
            _context = context;
        }
        [BindProperty]
        public TblContent TblContent { get; set; }
        public TblContent TblEvent { get; set; }
        public IList<TblContent> TblMyEvent { get; set; }
        public List<TblContent>? TblAttendees { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            TblMyEvent = await _context.TblContents
                .Where(x => x.FromId.Equals(User.Identity.Name))
                .Include(t => t.Parent)
                .Include(t => t.Category).ToListAsync();

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
            //var Attendee = await _context.TblContents.FirstOrDefaultAsync(m => m.ParentId == id && m.FromId == User.Identity.Name);
            var Attendees = await _context.TblContents.Where(m => m.ParentId == id).ToListAsync();

            //if (Attendee != null)
            //{
            //    TblContent = Attendee;
            //}
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
            TblContent.ParentId = id;


            if (TblContent.IsActive == false)
            {
                //Delete
                TblContent.Title = null;
            }
            if (User.Identity.Name != null)
            {
                TblContent.FromId = User.Identity.Name;
            }

            await _context.Procedures.usp_EventAttendAsync(TblContent.Id, TblContent.FromId, id, /* <-- Parent ID */
                TblContent.Title, TblContent.ShortDescription, TblContent.Price, TblContent.StartDate, TblContent.EndDate, TblContent.Qty, TblContent.IsDefault, TblContent.IsArchived, "attendee", TblContent.Icon);

            _context.TblContents.Add(TblContent);
            await _context.SaveChangesAsync();

            await LoadEvents(id);

            //return RedirectToPage("./Details1", new { id = TblContent.ParentId });
            return Redirect($"{Request.Path.ToString()}{Request.QueryString.Value.ToString()}");
        }
    }
}
