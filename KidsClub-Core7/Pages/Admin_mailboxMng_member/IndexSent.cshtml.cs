﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KidsClub_Core7.Data;

namespace KidsClub.Pages.Admin_mailboxMng_member
{
    public class IndexSentModel : PageModel
    {
        private readonly KidsClub_Core7.Data.KidsClubContext _context;

        public IndexSentModel(KidsClub_Core7.Data.KidsClubContext context)
        {
            _context = context;
        }

        public IList<TblContent> TblContent { get; set; }

        public async Task OnGetAsync()
        {
            TblContent = await _context.TblContents.Where(x => x.CategoryId.Equals(45)).ToListAsync();// && x.FromId == User.Identity.Name && x.ToId != User.Identity.Name)
                                                                                                      //.Include(t => t.Category)
                                                                                                      //.Include(t => t.Parent).ToListAsync();
        }
    }
}
