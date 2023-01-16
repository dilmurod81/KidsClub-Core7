using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KidsClub.EFCorePowerTool.Entities;
using KidsClub.EFCorePowerTool.Context;
//using KidsClub_Core7.Data;

namespace KidsClub.Pages.Admin_profile_member
{
    public class IndexModel : PageModel
    {
        private readonly KidsClubContext _context;

        public IndexModel(KidsClubContext context)
        {
            _context = context;
        }
        [BindProperty]
        public TblContent TblContent { get; set; }// <-- Attendee (Single)
        public IList<TblContent> TblContents { get; set; }
        public IList<AspNetUsers> AspNetUser { get; set; }
        public TblContent TblRe { get; set; }
        public IList<VwContentChildCountMurod> TblCount { get; set; }// <-- Event (Single)
        public IList<VwContentChildCountMurod> TblUsers { get; set; }// <-- Event (Single)
        public IList<VwContentChildCountMurod> TblSender { get; set; }// <-- Event (Single)
        public IList<VwContentChildCountMurod> TblSenderInitial { get; set; }// <-- Event (Single)
        public VwContentChildCountMurod vwContentChildCountMurod { get; set; }// <-- Event (Single)
        public List<TblContent>? TblAttendees { get; set; } = default!;// <-- Attendees (Multi)
        public VwContentChildCountMurod TblEvent { get; private set; }

        public async Task<IActionResult> OnGetAsync(int? id, string FromId)
        {

            //if (id == null || _context.TblContents == null)
            //{
            //    return NotFound();
            //}
            TblCount = await _context.VwContentChildCountMurod.ToListAsync();
            TblSender = await _context.VwContentChildCountMurod
                .Where(x => x.CategoryId == 21 && x.ParentId != null && x.FromId != User.Identity.Name && x.ToId == User.Identity.Name).ToListAsync();
            TblSenderInitial = await _context.VwContentChildCountMurod
                .Where(x => x.CategoryId == 21 && x.ParentId == null && x.FromId != User.Identity.Name && x.ToId == User.Identity.Name).ToListAsync();
            TblUsers = await _context.VwContentChildCountMurod
                .Where(x => x.CategoryId == 21 && x.FromId != User.Identity.Name && x.ToId == User.Identity.Name).ToListAsync();
            AspNetUser = await _context.AspNetUsers
                .Where(x => x.UserName.Equals(User.Identity.Name)).ToListAsync();
            //TblResponse = await _context.TblContents
            //    .Where(x => x.CategoryId == 21 && x.FromId == User.Identity.Name && x.ParentId == id).ToListAsync();
            await LoadEvents(id);

            return Page();
        }

        private async Task LoadEvents(int? id)
        {
            var Event = await _context.VwContentChildCountMurod.FirstOrDefaultAsync(m => m.Id == id);
            var Comments = await _context.VwContentChildCountMurod.FirstOrDefaultAsync(m => m.Id == id);
            var Attendee = await _context.TblContents.FirstOrDefaultAsync(m => m.ParentId == id && m.FromId == User.Identity.Name);
            var Re = await _context.TblContents.FirstOrDefaultAsync(m => m.CategoryId == 21 && m.ParentId == id && m.FromId == User.Identity.Name);
            var Attendees = await _context.TblContents.Where(m => m.ParentId == id).ToListAsync();

            if (Re != null)
            {
                TblRe = Re;
            }
            if (Event != null)
            {
                TblEvent = Event;
            }
            if (Comments != null)
            {
                vwContentChildCountMurod = Comments;
            }
            if (Attendees != null)
            {
                TblAttendees = Attendees.ToList();
            }
        }


        public async Task<IActionResult> OnPostAsync(int? id, string? FromId)
        {

            TblContent.IsActive = true;
            TblContent.CategoryId = 21;
            TblContent.ParentId = id;
            TblContent.FromId = User.Identity.Name;
            TblContent.ToId = FromId;
            //TblContent.Picture = AspNetUser.Where(x => x.Email == TblContent.ToId).ElementAt(17).ToString();

            //if (TblContent.IsActive == false)
            //{
            //    //Delete
            //    TblContent.Title = null;
            //}
            //if (User.Identity.Name == null)
            //{
            //    await LoadEvents(id);
            //    return Page();
            //}

            await _context.Procedures.usp_Content_InsertAsync(TblContent.ParentId, TblContent.CategoryId, TblContent.FromId, TblContent.ToId, TblContent.Title, TblContent.ShortDescription, TblContent.LongDescription, TblContent.Url, TblContent.Urlslug, TblContent.Picture, TblContent.Icon, TblContent.DisplayOrder, TblContent.Qty, TblContent.Price, TblContent.DiscountPrice, TblContent.IsActive, TblContent.IsDefault, TblContent.IsArchived, TblContent.StartDate, TblContent.EndDate, TblContent.DateUpdated, TblContent.DateEntered);

            _context.TblContents.Add(TblContent);
            //TblContent.ParentId = id;
            //await _context.SaveChangesAsync();

            await LoadEvents(id);

            return RedirectToPage("/Admin_profile_member/Index", new { id = id });

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
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.EntityFrameworkCore;
////using KidsClub.EFCorePowerTool.Context;
////using KidsClub.EFCorePowerTool.Entities;
//using KidsClub_Core7.Data;

//namespace KidsClub.Pages.Admin_profile_member
//{
//    public class IndexModel : PageModel
//    {
//        private readonly KidsClub_Core7.Data.KidsClubContext _context;

//        public IndexModel(KidsClub_Core7.Data.KidsClubContext context)
//        {
//            _context = context;
//        }

//        public IList<AspNetUser> AspNetUser { get; set; }
//        public IList<AspNetUser> AspNetUsers { get; set; }
//        public IList<TblContent> TblContents { get; set; }
//        public TblContent Test { get; set; }
//        public IList<TblContent> TblResponse { get; set; }

//        public async Task OnGetAsync(int? id)
//        {

//            AspNetUser = await _context.AspNetUsers
//                .Where(x => x.UserName.Equals(User.Identity.Name)).ToListAsync();

//            AspNetUsers = await _context.AspNetUsers.ToListAsync();

//            TblContents = await _context.TblContents
//                .Where(x => x.CategoryId == 21 && x.ToId == User.Identity.Name).ToListAsync();

//            //TblResponse = await _context.TblContents
//            //    .Where(x => x.CategoryId == 21).FirstOrDefaultAsync(m => m.Id == id);

//            TblResponse = await _context.TblContents
//                .Where(x => x.CategoryId == 21 && x.FromId == User.Identity.Name && x.ParentId == id).ToListAsync();

//        }
//    }
//}
