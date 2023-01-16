using System;
using System.Collections.Generic;

namespace KidsClub_Core7.Data
{
    public partial class TblCategory
    {
        public TblCategory()
        {
            InverseParent = new HashSet<TblCategory>();
            TblContents = new HashSet<TblContent>();
        }

        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string? Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? Picture { get; set; }
        public bool? IsActive { get; set; }
        public DateTime DateEntered { get; set; }

        public virtual TblCategory? Parent { get; set; }
        public virtual ICollection<TblCategory> InverseParent { get; set; }
        public virtual ICollection<TblContent> TblContents { get; set; }
    }
}
