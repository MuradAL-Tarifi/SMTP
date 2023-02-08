using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMTP.Models
{
    public partial class Smtpchecker
    {
        public long Id { get; set; }
        public string Serial { get; set; }
        public bool? IsSendTemperature { get; set; }
        public bool? IsSendHumidity { get; set; }
        public bool? IsSendTemperatureSecond { get; set; }
        public bool? IsSendHumiditySecond { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
