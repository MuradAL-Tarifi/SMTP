using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMTP.ThirdModels
{
    public partial class Gateway
    {
        public Gateway()
        {
            Inventory = new HashSet<Inventory>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Imei { get; set; }
        public string Simnumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int BrandId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? ActivationDate { get; set; }
        public DateTime? SimcardExpirationDate { get; set; }
        public int? NumberOfMonths { get; set; }
        public string Notes { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual ICollection<Inventory> Inventory { get; set; }
    }
}
