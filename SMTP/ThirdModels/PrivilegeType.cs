using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMTP.ThirdModels
{
    public partial class PrivilegeType
    {
        public PrivilegeType()
        {
            UserPrivilege = new HashSet<UserPrivilege>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEn { get; set; }
        public bool IsDeleted { get; set; }
        public int? Order { get; set; }
        public string RoleId { get; set; }
        public bool Editable { get; set; }

        public virtual ICollection<UserPrivilege> UserPrivilege { get; set; }
    }
}
