using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMTP.ThirdModels
{
    public partial class WaslCode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string MessageAr { get; set; }
        public string MessageEn { get; set; }
    }
}
