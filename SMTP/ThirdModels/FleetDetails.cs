using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMTP.ThirdModels
{
    public partial class FleetDetails
    {
        public long Id { get; set; }
        public long FleetId { get; set; }
        public string IdentityNumber { get; set; }
        public string DateOfBirthHijri { get; set; }
        public string CommercialRecordNumber { get; set; }
        public string CommercialRecordIssueDateHijri { get; set; }
        public string PhoneNumber { get; set; }
        public string ExtensionNumber { get; set; }
        public string EmailAddress { get; set; }
        public string ManagerName { get; set; }
        public string ManagerPhoneNumber { get; set; }
        public string ManagerMobileNumber { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string ReferanceNumber { get; set; }
        public bool IsLinkedWithWasl { get; set; }
        public string ActivityType { get; set; }
        public string SfdacompanyActivities { get; set; }
        public string FleetType { get; set; }
    }
}
