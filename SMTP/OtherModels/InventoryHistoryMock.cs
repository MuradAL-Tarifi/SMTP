using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMTP.OtherModels
{
    public partial class InventoryHistoryMock
    {
        public long Id { get; set; }
        public long InventoryId { get; set; }
        public long GatewayImei { get; set; }
        public string Serial { get; set; }
        public decimal? Temperature { get; set; }
        public decimal? Humidity { get; set; }
        public bool? IsLowVoltage { get; set; }
        public DateTime GpsDate { get; set; }
        public string Alram { get; set; }
        public string Gsmstatus { get; set; }
    }
}
