using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KidsClub.EFCorePowerTool.Context;
using KidsClub.EFCorePowerTool.Entities;

namespace KidsClub.Pages.Admin_messages
{
    public class DetailsModel : PageModel
    {
        private readonly KidsClub.EFCorePowerTool.Context.KidsClubContext _context;

        public DetailsModel(KidsClub.EFCorePowerTool.Context.KidsClubContext context)
        {
            _context = context;
        }
        [BindProperty]
        public TblContent TblReplyer { get; set; }// <-- Attendee (Single)
        public TblContent TblMessage { get; set; }// <-- Testimony (Single)
        //public List<TblContent>? TblComments { get; set; } = default!;// <-- Comments (Multi)

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

            var Message = await _context.TblContents.FirstOrDefaultAsync(m => m.Id == id);
            var Replyer = await _context.TblContents.FirstOrDefaultAsync(m => m.ParentId == id && m.FromId == User.Identity.Name);
            //var Comments = await _context.TblContents.Where(m => m.ParentId == id).ToListAsync();
            //var Comments = await _context.TblContents.Where(m => m.CategoryId == 1 && m.ParentId != null).ToListAsync();

            if (Replyer != null)
            {
                TblReplyer = Replyer;
            }
            if (Message != null)
            {
                TblMessage = Message;
            }
            //if (Comments != null)
            //{
            //    TblComments = Comments.ToList();
            //}
        }


        public async Task<IActionResult> OnPostAsync(int? id)
        {

            TblReplyer.IsActive = true;

            if (TblReplyer.IsActive == false)
            {
                //Delete
                TblReplyer.Title = null;
            }
            if (User.Identity.Name == null)
            {
                await LoadEvents(id);
                return Page();
            }
            TblReplyer.FromId = User.Identity.Name;
            TblReplyer.ParentId = id;


            await _context.Procedures.usp_TestimonialsAsync(TblReplyer.Id, TblReplyer.ParentId, TblReplyer.FromId, TblReplyer.ToId,
            TblReplyer.Title, TblReplyer.ShortDescription, TblReplyer.LongDescription, TblReplyer.DisplayOrder, TblReplyer.Qty, "Messages");


            //_context.TblContents.Add(TblContent);
            //await _context.SaveChangesAsync();

            await LoadEvents(id);

            return RedirectToPage("./Index");

            //return Page();
        }

    }
}
//public class DetailsModel : PageModel
//{
//    private readonly KidsClub.EFCorePowerTool.Context.KidsClubContext _context;

//    public DetailsModel(KidsClub.EFCorePowerTool.Context.KidsClubContext context)
//    {
//        _context = context;
//    }

//    public TblContent TblContent { get; set; }

//    public async Task<IActionResult> OnGetAsync(int? id)
//    {
//        if (id == null)
//        {
//            return NotFound();
//        }

//        TblContent = await _context.TblContents
//            .Include(t => t.Category)
//            .Include(t => t.Parent).FirstOrDefaultAsync(m => m.Id == id);

//        if (TblContent == null)
//        {
//            return NotFound();
//        }
//        return Page();
//    }
//}

