using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMTP.ThirdModels
{
    public partial class WaslIntegrationLog
    {
        public long Id { get; set; }
        public int HttpCode { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public DateTime LogDate { get; set; }
        public int WaslLogTypeLookupId { get; set; }
    }
}
