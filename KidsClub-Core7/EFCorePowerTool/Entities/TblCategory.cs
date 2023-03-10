// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace KidsClub.EFCorePowerTool.Entities
{
    public partial class TblCategory
    {
        public TblCategory()
        {
            InverseParent = new HashSet<TblCategory>();
        }

        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Picture { get; set; }
        public bool? IsActive { get; set; }
        public DateTime DateEntered { get; set; }

        public virtual TblCategory Parent { get; set; }
        public virtual ICollection<TblCategory> InverseParent { get; set; }
    }
}