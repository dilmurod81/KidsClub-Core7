// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace KidsClub.EFCorePowerTool.Entities
{
    public partial class VwMessages
    {
        public int MessageId { get; set; }
        public string FromId { get; set; }
        public string SenderId { get; set; }
        public string Sender { get; set; }
        public string ToId { get; set; }
        public string ReceiverId { get; set; }
        public string Receiver { get; set; }
        public string Message { get; set; }
        public DateTime? DateEntered { get; set; }
        public bool? IsActive { get; set; }
    }
}