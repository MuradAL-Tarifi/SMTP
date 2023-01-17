using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMTP.ThirdModels
{
    public partial class InventorySensor
    {
        public InventorySensor()
        {
            SensorAlert = new HashSet<SensorAlert>();
        }

        public long Id { get; set; }
        public long InventoryId { get; set; }
        public long? SensorId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public virtual Inventory Inventory { get; set; }
        public virtual Sensor Sensor { get; set; }
        public virtual ICollection<SensorAlert> SensorAlert { get; set; }
    }
}
