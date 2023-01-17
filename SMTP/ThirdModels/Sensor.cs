using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMTP.ThirdModels
{
    public partial class Sensor
    {
        public Sensor()
        {
            InventorySensor = new HashSet<InventorySensor>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Serial { get; set; }
        public int BrandId { get; set; }
        public DateTime? CalibrationDate { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual ICollection<InventorySensor> InventorySensor { get; set; }
    }
}
