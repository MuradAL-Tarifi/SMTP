using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMTP.ThirdModels
{
    public partial class ReportTypeLookup
    {
        public ReportTypeLookup()
        {
            ReportSchedule = new HashSet<ReportSchedule>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<ReportSchedule> ReportSchedule { get; set; }
    }
}
