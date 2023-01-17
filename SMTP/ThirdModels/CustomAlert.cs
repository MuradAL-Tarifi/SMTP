using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMTP.ThirdModels
{
    public partial class CustomAlert
    {
        public CustomAlert()
        {
            Alert = new HashSet<Alert>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public double? MinValueTemperature { get; set; }
        public double? MaxValueTemperature { get; set; }
        public double? MinValueHumidity { get; set; }
        public double? MaxValueHumidity { get; set; }
        public int Interval { get; set; }
        public string ToEmails { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? LastAlertDate { get; set; }
        public int AlertTypeLookupId { get; set; }
        public long FleetId { get; set; }
        public string UserIds { get; set; }

        public virtual AlertTypeLookup AlertTypeLookup { get; set; }
        public virtual Fleet Fleet { get; set; }
        public virtual ICollection<Alert> Alert { get; set; }
    }
}
