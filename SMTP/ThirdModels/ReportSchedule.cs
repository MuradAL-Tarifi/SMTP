using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMTP.ThirdModels
{
    public partial class ReportSchedule
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public long FleetId { get; set; }
        public int ReportTypeLookupId { get; set; }
        public long? VehicleId { get; set; }
        public long? GeofenceId { get; set; }
        public long? WarehouseId { get; set; }
        public long? InventoryId { get; set; }
        public long? SensorSerial { get; set; }
        public int? AlertTypeLookupId { get; set; }
        public string Name { get; set; }
        public bool? Engine { get; set; }
        public int? SpeedFrom { get; set; }
        public int? SpeedTo { get; set; }
        public bool NewerToOlder { get; set; }
        public bool? Daily { get; set; }
        public bool? Weekly { get; set; }
        public bool? Monthly { get; set; }
        public bool? Yearly { get; set; }
        public int? DayOfWeekId { get; set; }
        public int? DayOfMonthId { get; set; }
        public int? DailyRepeat { get; set; }
        public int? WeeklyRepeat { get; set; }
        public int? MonthlyRepeat { get; set; }
        public string DailyTime { get; set; }
        public string WeeklyTime { get; set; }
        public string MonthlyTime { get; set; }
        public string Emails { get; set; }
        public bool IsActive { get; set; }
        public bool IsEnglish { get; set; }
        public bool? Pdf { get; set; }
        public bool? Excel { get; set; }
        public string GroupUpdatesByType { get; set; }
        public int? GroupUpdatesValue { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public virtual DayOfWeekLookup DayOfWeek { get; set; }
        public virtual Fleet Fleet { get; set; }
        public virtual Inventory Inventory { get; set; }
        public virtual ReportTypeLookup ReportTypeLookup { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
