using KidsClub_Core7.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KidsClub.Pages.Gallery
{

    public class VideoModel : PageModel
    {
        private readonly KidsClub_Core7.Data.KidsClubContext _context;

        public VideoModel(KidsClub_Core7.Data.KidsClubContext context)
        {
            _context = context;
        }

        public IList<TblContent> TblContent { get; set; }

        public async Task OnGetAsync()
        {
            TblContent = await _context.TblContents.Where(x => x.CategoryId.Equals(108))
                .Include(t => t.Category)
                .Include(t => t.Parent).ToListAsync();
        }
    }


}
