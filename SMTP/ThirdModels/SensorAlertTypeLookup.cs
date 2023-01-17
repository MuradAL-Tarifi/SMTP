using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMTP.ThirdModels
{
    public partial class SensorAlertTypeLookup
    {
        public SensorAlertTypeLookup()
        {
            SensorAlert = new HashSet<SensorAlert>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public int? RowOrder { get; set; }
        public bool IsRange { get; set; }
        public string DataType { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<SensorAlert> SensorAlert { get; set; }
    }
}
