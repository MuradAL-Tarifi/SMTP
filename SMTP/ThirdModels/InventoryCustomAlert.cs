using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMTP.ThirdModels
{
    public partial class InventoryCustomAlert
    {
        public long Id { get; set; }
        public long InventorytId { get; set; }
        public long CustomAlertId { get; set; }
    }
}
