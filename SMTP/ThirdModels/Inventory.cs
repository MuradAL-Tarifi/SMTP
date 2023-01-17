using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMTP.ThirdModels
{
    public partial class Inventory
    {
        public Inventory()
        {
            InventorySensor = new HashSet<InventorySensor>();
            ReportSchedule = new HashSet<ReportSchedule>();
            UserInventory = new HashSet<UserInventory>();
        }

        public long Id { get; set; }
        public long WarehouseId { get; set; }
        public long GatewayId { get; set; }
        public string Name { get; set; }
        public string InventoryNumber { get; set; }
        public bool IsActive { get; set; }
        public int? RegisterTypeId { get; set; }
        public string WaslActivityType { get; set; }
        public string SfdastoringCategory { get; set; }
        public bool IsLinkedWithWasl { get; set; }
        public string ReferenceKey { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public virtual Gateway Gateway { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual ICollection<InventorySensor> InventorySensor { get; set; }
        public virtual ICollection<ReportSchedule> ReportSchedule { get; set; }
        public virtual ICollection<UserInventory> UserInventory { get; set; }
    }
}
