using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace SMTP.ThirdModels
{
    public partial class SystemSetting
    {
        public long Id { get; set; }
        public byte[] LogoPhotoByte { get; set; }
        public string CompanyName { get; set; }
        public string GoogleApiKey { get; set; }
        public string WaslApiKey { get; set; }
        public bool EnableSmtp { get; set; }
        public string SmtpHost { get; set; }
        public int? SmtpPort { get; set; }
        public bool SmtpIsSslEnabled { get; set; }
        public string SmtpAddress { get; set; }
        public string SmtpDisplayName { get; set; }
        public string SmtpPassword { get; set; }
        public bool EnableSms { get; set; }
        public string SmsGatewayUrl { get; set; }
        public string SmsPassword { get; set; }
        public string SmsUsername { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}
