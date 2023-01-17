using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMTP.ThirdModels
{
    public partial class User
    {
        public User()
        {
            EventLog = new HashSet<EventLog>();
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public int? AgentId { get; set; }
        public long? FleetId { get; set; }
        public string AppId { get; set; }
        public bool EnableMobileAlerts { get; set; }
        public bool IsSubAdminAgent { get; set; }
        public bool IsSuperAdmin { get; set; }

        public virtual Agent Agent { get; set; }
        public virtual Fleet Fleet { get; set; }
        public virtual ICollection<EventLog> EventLog { get; set; }
    }
}
