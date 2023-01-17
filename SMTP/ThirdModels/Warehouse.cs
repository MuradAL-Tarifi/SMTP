using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMTP.ThirdModels
{
    public partial class Warehouse
    {
        public Warehouse()
        {
            Inventory = new HashSet<Inventory>();
            ReportSchedule = new HashSet<ReportSchedule>();
            UserWarehouse = new HashSet<UserWarehouse>();
        }

        public long Id { get; set; }
        public long FleetId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string LandCoordinates { get; set; }
        public double LandAreaInSquareMeter { get; set; }
        public string LicenseNumber { get; set; }
        public string LicenseIssueDate { get; set; }
        public string LicenseExpiryDate { get; set; }
        public string ManagerMobile { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string ReferenceKey { get; set; }
        public string WaslActivityType { get; set; }
        public bool IsLinkedWithWasl { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public virtual Fleet Fleet { get; set; }
        public virtual ICollection<Inventory> Inventory { get; set; }
        public virtual ICollection<ReportSchedule> ReportSchedule { get; set; }
        public virtual ICollection<UserWarehouse> UserWarehouse { get; set; }
    }
}
