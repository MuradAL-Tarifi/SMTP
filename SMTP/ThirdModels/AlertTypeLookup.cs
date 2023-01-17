using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMTP.ThirdModels
{
    public partial class AlertTypeLookup
    {
        public AlertTypeLookup()
        {
            CustomAlert = new HashSet<CustomAlert>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public int? RowOrder { get; set; }
        public bool IsRange { get; set; }
        public bool HasMinValue { get; set; }
        public bool HasMaxValue { get; set; }
        public string DataType { get; set; }
        public string Unit { get; set; }
        public string UnitEn { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<CustomAlert> CustomAlert { get; set; }
    }
}
