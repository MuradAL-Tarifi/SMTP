using System.Net.Mail;
using System.Net;
using System;
using SMTP.Models;
using Microsoft.Extensions.Configuration;

namespace SMTP.Service
{
    public class MailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public bool SendEmailAlarm(string to, AlertTracker alertTracker)
        {
            using (SmtpClient smtpClient = new SmtpClient())
            {
                var basicCredential = new NetworkCredential(_configuration["SMTPSetting:UserName"], _configuration["SMTPSetting:Password"]);
                using (MailMessage message = new MailMessage())
                {
                    MailAddress fromAddress = new MailAddress(_configuration["SMTPSetting:MailAddress"]);

                    smtpClient.Host = _configuration["SMTPSetting:Host"];
                    smtpClient.Port = Convert.ToInt32(_configuration["SMTPSetting:Port"]);
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = basicCredential;
                    //smtpClient.EnableSsl = true;

                    message.From = fromAddress;
                    message.Subject = "Quality Compliance";
                    message.IsBodyHtml = true;
                    message.Body = $@"    
<div style=""font-family: 'Times New Roman', Times, serif;font-size: 15px;"">
        <pre>Hello {alertTracker.UserName},<br>An alarm has been triggered on Accu Tracking<br>Date               {alertTracker.AlertDateTime.Value.ToString("dddd, MMMM, dd, yyyy hh:mm tt")}<br>Type               {alertTracker.AlertType}<br>Monitored unit     {alertTracker.MonitoredUnit}<br>Alarm measurement  {alertTracker.MessageForValue}<br>Recorder           {alertTracker.Serial}<br>Zone               {alertTracker.Zone}<br>Batches            {alertTracker.WarehouseName}<br><br>Regards,<br>Your alert system of Accu Tracking<br><br><small>This message has been generated automatically. Please do not reply</small></pre>
    </div>";
                    message.To.Add(to);

                    try
                    {
                        smtpClient.Send(message);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
        }
        public bool SendEmailIsLowVoltage(string to, string sensiorName, string invName, string warehouseName)
        {
            using (SmtpClient smtpClient = new SmtpClient())
            {
                var basicCredential = new NetworkCredential(_configuration["SMTPSetting:UserName"], _configuration["SMTPSetting:Password"]);
                using (MailMessage message = new MailMessage())
                {
                    MailAddress fromAddress = new MailAddress(_configuration["SMTPSetting:MailAddress"]);

                    smtpClient.Host = _configuration["SMTPSetting:Host"];
                    smtpClient.Port = Convert.ToInt32(_configuration["SMTPSetting:Port"]);
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = basicCredential;
                    //smtpClient.EnableSsl = true;

                    message.From = fromAddress;
                    message.Subject = "Quality Compliance";
                    message.IsBodyHtml = true;
                    message.Body = $@"    
<div style=""font-family: 'Times New Roman', Times, serif;font-size: 15px;"">
        <pre>Dear sir,<br>An alarm has been triggered on Accu Tracking<br>Kindly note that the battery of Sensor {sensiorName} at Warehouse {warehouseName} on Inventory {invName} is almost dead<br><br>Regards,<br>Your alert system of Accu Tracking<br><br><small>This message has been generated automatically. Please do not reply</small></pre>
    </div>";
                    message.To.Add(to);

                    try
                    {
                        smtpClient.Send(message);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
        }
        public bool SendEmailIsNotActive(string to, string sensiorName,string invName, string warehouseName)
        {
            using (SmtpClient smtpClient = new SmtpClient())
            {
                var basicCredential = new NetworkCredential(_configuration["SMTPSetting:UserName"], _configuration["SMTPSetting:Password"]);
                using (MailMessage message = new MailMessage())
                {
                    MailAddress fromAddress = new MailAddress(_configuration["SMTPSetting:MailAddress"]);

                    smtpClient.Host = _configuration["SMTPSetting:Host"];
                    smtpClient.Port = Convert.ToInt32(_configuration["SMTPSetting:Port"]);
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = basicCredential;
                    //smtpClient.EnableSsl = true;

                    message.From = fromAddress;
                    message.Subject = "Quality Compliance";
                    message.IsBodyHtml = true;
                    message.Body = $@"    
<div style=""font-family: 'Times New Roman', Times, serif;font-size: 15px;"">
        <pre>Dear sir,<br>An alarm has been triggered on Accu Tracking<br>Kindly note that the Sensor {sensiorName} at Warehouse {warehouseName} on Inventory {invName} not working<br><br>Regards,<br>Your alert system of Accu Tracking<br><br><small>This message has been generated automatically. Please do not reply</small></pre>
    </div>";
                    message.To.Add(to);

                    try
                    {
                        smtpClient.Send(message);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }
        }

    }
}
