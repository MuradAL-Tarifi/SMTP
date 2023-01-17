using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMTP.ThirdModels
{
    public partial class Brand
    {
        public Brand()
        {
            Gateway = new HashSet<Gateway>();
            Sensor = new HashSet<Sensor>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<Gateway> Gateway { get; set; }
        public virtual ICollection<Sensor> Sensor { get; set; }
    }
}
