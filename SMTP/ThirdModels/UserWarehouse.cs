using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMTP.ThirdModels
{
    public partial class UserWarehouse
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public long WarehouseId { get; set; }

        public virtual Warehouse Warehouse { get; set; }
    }
}
