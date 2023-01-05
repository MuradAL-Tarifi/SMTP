using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SMTP.Service;

namespace SMTP
{
    public class Background : IHostedService, IDisposable
    {
        private Timer timer60Seconds;
        private Timer timer1Day;


        private readonly AlertTrackerService _processBackground;

        public Background(AlertTrackerService processBackground)
        {
            _processBackground = processBackground;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {

            timer1Day = new Timer(async o =>
            {
                //var x = MailService.SendEmail("Khalid@compliance.com.sa");
                var alertTrackersNotTracked =await _processBackground.GetAlertTrackersNotTrackedAsync();
                if (alertTrackersNotTracked != null)
                {
                    foreach (var item in alertTrackersNotTracked)
                    {
                        var emails = item.SendTo.Split(",");
                        if (emails != null)
                        {
                            bool result = false;
                            foreach (var email in emails)
                            {
                                result = MailService.SendEmail(email,item);
                            }
                            if (result)
                            {
                                await _processBackground.UpdateAlertTrackerToSendAsync(item.Id);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Email should not be Null");
                        }
                    }
                    Console.WriteLine("Done");

                }
                else
                {
                    Console.WriteLine("No Thing Found");
                }


            },
           null,
           TimeSpan.Zero,
           TimeSpan.FromMinutes(10)
           );


            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("printing worker stoping");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer60Seconds?.Dispose();
            timer1Day?.Dispose();
        }
    }
}
