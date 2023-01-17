using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMTP.ThirdModels
{
    public partial class EventLog
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public string ObjectId { get; set; }
        public string ObjectType { get; set; }
        public string Data { get; set; }
        public string UserId { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual User User { get; set; }
    }
}
