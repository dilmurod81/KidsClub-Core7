﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace KidsClub.EFCorePowerTool.Entities
{
    public partial class AspNetRole
    {
        public AspNetRole()
        {
            AspNetRoleClaim = new HashSet<AspNetRoleClaim>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string Description { get; set; }
        public string ConcurrencyStamp { get; set; }
        public bool? IsActive { get; set; }
        public DateTime DateEntered { get; set; }

        public virtual ICollection<AspNetRoleClaim> AspNetRoleClaim { get; set; }
    }
}