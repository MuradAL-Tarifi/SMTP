using System.Net.Mail;
using System.Net;
using System;
using SMTP.Models;

namespace SMTP.Service
{
    public class MailService
    {
        public static bool SendEmail(string to, AlertTracker alertTracker)
        {
            using (SmtpClient smtpClient = new SmtpClient())
            {
                var basicCredential = new NetworkCredential("a.abugharbia@compliance.com.sa", "IODCC5zv");
                using (MailMessage message = new MailMessage())
                {
                    MailAddress fromAddress = new MailAddress("noreply@accu-tracking.com");

                    smtpClient.Host = "pro.turbo-smtp.com";
                    smtpClient.Port = 587;
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
//        public static bool SendEmail(string to)
//        {
//            using (SmtpClient smtpClient = new SmtpClient())
//            {
//                var basicCredential = new NetworkCredential("mailclass-uaabaijec@accu-tracking.com", "6593403Ahmed");
//                using (MailMessage message = new MailMessage())
//                {
//                    MailAddress fromAddress = new MailAddress("noreply@accu-tracking.com");

//                    smtpClient.Host = "akoneseo.com";
//                    smtpClient.Port = 587;
//                    smtpClient.UseDefaultCredentials = false;
//                    smtpClient.Credentials = basicCredential;

//                    message.From = fromAddress;
//                    message.Subject = "Quality Compliance";
//                    message.IsBodyHtml = true;

//                    message.Body = @"    
//<div style=""font-family: 'Times New Roman', Times, serif;font-size: 15px;"">
//        <pre>Hello Murad,<br>An alarm has been triggered on Accu Tracking<br>Date               Monday, January 2, 2023 5:48 AM<br>Type               Continuous high threshold<br>Monitored unit     Waerhouse JED(AD19206439)<br>Measurement point  DL-11<br>Alarm measurement  65.06%<br>Recorder           AD19206439<br>Zone               Cigalah<br>Batches            Waerhouse JED;<br><br>Regards,<br>Your alert system of Accu Tracking<br><br><small>This message has been generated automatically. Please do not reply</small></pre>
//    </div>";
//                    message.To.Add(to);

//                    try
//                    {
//                        smtpClient.Send(message);
//                        return true;
//                    }
//                    catch (Exception ex)
//                    {
//                        return false;
//                    }
//                }
//            }
//        }
    }
}
