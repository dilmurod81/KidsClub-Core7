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

namespace KidsClub.Pages.Admin_pictures_member
{
    public class DetailsModel : PageModel
    {
        private readonly KidsClubContext _context;

        public DetailsModel(KidsClubContext context)
        {
            _context = context;
        }
        [BindProperty]
        public TblContent TblCommentor { get; set; }// <-- Attendee (Single)
        public VwContentChildCountMurod TblPicture { get; set; }// <-- Testimony (Single)
        public VwContentChildCountMurod vwContentChildCountMurod { get; set; }// <-- Testimony (Single)
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

            var Picture = await _context.VwContentChildCountMurod.FirstOrDefaultAsync(m => m.Id == id);
            var CommentCount = await _context.VwContentChildCountMurod.FirstOrDefaultAsync(m => m.Id == id);
            //var Commentor = await _context.TblContents.FirstOrDefaultAsync(m => m.ParentId == id && m.FromId == User.Identity.Name);
            var Comments = await _context.TblContents.Where(m => m.ParentId == id).ToListAsync();
            //var Comments = await _context.TblContents.Where(m => m.CategoryId == 1 && m.ParentId != null).ToListAsync();

            if (CommentCount != null)
            {
                vwContentChildCountMurod = CommentCount;
            }
            if (Picture != null)
            {
                TblPicture = Picture;
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


            await _context.Procedures.usp_TestimonialsAsync(TblCommentor.Id, TblCommentor.ParentId, TblCommentor.FromId, TblCommentor.ToId,
            TblCommentor.Title, TblCommentor.ShortDescription, TblCommentor.LongDescription, TblCommentor.DisplayOrder, TblCommentor.Qty, "Pictures");


            //_context.TblContents.Add(TblContent);
            //await _context.SaveChangesAsync();

            await LoadEvents(id);

            return RedirectToPage("./Details", new { id = id });

            //return Page();
        }
        public async Task<IActionResult> OnGetDelete(int? commentorId)
        {
            int? ParentId = 0;
            if (commentorId != null)
            {

                var Commentor = await _context.TblContents.FirstOrDefaultAsync(m => m.Id == commentorId);// && m.FromId == User.Identity.Name);
                if (Commentor != null)
                {
                    ParentId = Commentor.ParentId;
                    _context.TblContents.Remove(Commentor);
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
//using KidsClub_Core7.Data;

//namespace KidsClub.Pages.Admin_pictures_member
//{
//    public class DetailsModel : PageModel
//    {
//        private readonly KidsClub_Core7.Data.KidsClubContext _context;

//        public DetailsModel(KidsClub_Core7.Data.KidsClubContext context)
//        {
//            _context = context;
//        }

//        public TblContent TblContent { get; set; }

//        public async Task<IActionResult> OnGetAsync(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            TblContent = await _context.TblContents
//                .Include(t => t.Category)
//                .Include(t => t.Parent).FirstOrDefaultAsync(m => m.Id == id);

//            if (TblContent == null)
//            {
//                return NotFound();
//            }
//            return Page();
//        }
//    }
//}
