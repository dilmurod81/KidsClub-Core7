using System;
using System.Collections.Generic;

namespace KidsClub_Core7.Data
{
    public partial class AspNetRole
    {
        public AspNetRole()
        {
            AspNetRoleClaims = new HashSet<AspNetRoleClaim>();
        }

        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? NormalizedName { get; set; }
        public string? Description { get; set; }
        public string ConcurrencyStamp { get; set; } = null!;
        public bool? IsActive { get; set; }
        public DateTime DateEntered { get; set; }

        public virtual ICollection<AspNetRoleClaim> AspNetRoleClaims { get; set; }
    }
}
