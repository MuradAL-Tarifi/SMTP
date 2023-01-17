using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMTP.ThirdModels
{
    public partial class EmailHistory
    {
        public long Id { get; set; }
        public long? AlertId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string ToEmails { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsSent { get; set; }
        public DateTime? SentDate { get; set; }
    }
}
