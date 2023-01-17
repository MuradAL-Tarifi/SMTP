using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMTP.ThirdModels
{
    public partial class Fleet
    {
        public Fleet()
        {
            CustomAlert = new HashSet<CustomAlert>();
            ReportSchedule = new HashSet<ReportSchedule>();
            User = new HashSet<User>();
            Warehouse = new HashSet<Warehouse>();
        }

        public long Id { get; set; }
        public int AgentId { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public string ManagerEmail { get; set; }
        public string ManagerMobile { get; set; }
        public string SupervisorEmail { get; set; }
        public string SupervisorMobile { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string TaxRegistrationNumber { get; set; }
        public string CommercialRegistrationNumber { get; set; }
        public byte[] LogoPhotoByte { get; set; }
        public string LogoPhotoExtention { get; set; }

        public virtual Agent Agent { get; set; }
        public virtual ICollection<CustomAlert> CustomAlert { get; set; }
        public virtual ICollection<ReportSchedule> ReportSchedule { get; set; }
        public virtual ICollection<User> User { get; set; }
        public virtual ICollection<Warehouse> Warehouse { get; set; }
    }
}
