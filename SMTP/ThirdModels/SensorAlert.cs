using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMTP.ThirdModels
{
    public partial class SensorAlert
    {
        public long Id { get; set; }
        public long InventorySensorId { get; set; }
        public int SensorAlertTypeLookupId { get; set; }
        public bool IsActive { get; set; }
        public bool IsSms { get; set; }
        public bool IsEmail { get; set; }
        public decimal? FromValue { get; set; }
        public decimal? ToValue { get; set; }

        public virtual InventorySensor InventorySensor { get; set; }
        public virtual SensorAlertTypeLookup SensorAlertTypeLookup { get; set; }
    }
}
