﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace KidsClub.EFCorePowerTool.Entities
{
    public partial class TblContent
    {
        public TblContent()
        {
            InverseParent = new HashSet<TblContent>();
        }

        public int Id { get; set; }
        public int? ParentId { get; set; }
        public int? CategoryId { get; set; }
        public string FromId { get; set; }
        public string ToId { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string Url { get; set; }
        public string Urlslug { get; set; }
        public string Picture { get; set; }
        public string Icon { get; set; }
        public byte? DisplayOrder { get; set; }
        public byte? Qty { get; set; }
        public decimal? Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDefault { get; set; }
        public bool IsArchived { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? DateUpdated { get; set; }
        public DateTime? DateEntered { get; set; }

        public virtual TblContent Parent { get; set; }
        public virtual TblCategory Category { get; set; }
        public virtual ICollection<TblContent> InverseParent { get; set; }
    }
}