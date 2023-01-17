using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SMTP.Service;
using SMTP.ThirdModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SMTP
{
    public class Background : IHostedService, IDisposable
    {
        private Timer timer10M;
        private Timer timer1Day;


        private readonly AlertTrackerService _processBackground;
        private readonly MailService mailService;
        private readonly TrackerDBContext trackerDBContext;

        public Background(AlertTrackerService processBackground, MailService mailService, TrackerDBContext trackerDBContext)
        {
            _processBackground = processBackground;
            this.mailService = mailService;
            this.trackerDBContext = trackerDBContext;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {

           // timer10M = new Timer(async o =>
           // {
           //     //var x = MailService.SendEmail("Khalid@compliance.com.sa");
           //     var alertTrackersNotTracked = await _processBackground.GetAlertTrackersNotTrackedAsync();
           //     if (alertTrackersNotTracked != null)
           //     {
           //         foreach (var item in alertTrackersNotTracked)
           //         {
           //             var emails = item.SendTo.Split(",");
           //             if (emails != null)
           //             {
           //                 bool result = false;
           //                 foreach (var email in emails)
           //                 {
           //                     result = mailService.SendEmailAlarm(email, item);
           //                     if (!result)
           //                     {
           //                         Console.WriteLine("SMTP ERROR Cant send to this email " + email);
           //                     }
           //                 }
           //                 if (result)
           //                 {
           //                     await _processBackground.UpdateAlertTrackerToSendAsync(item.Id);
           //                     Console.WriteLine("Done");
           //                 }
           //             }
           //             else
           //             {
           //                 Console.WriteLine("Email should not be Null");
           //             }
           //         }
           //     }
           //     else
           //     {
           //         Console.WriteLine("No Thing Found");
           //     }


           // },
           //null,
           //TimeSpan.Zero,
           //TimeSpan.FromMinutes(10)
           //);
            timer1Day = new Timer(async o =>
            {
                var serials = await _processBackground.GetInventoryHistoryNotActive();
                if (serials.Count != 0)
                {
                    foreach (var item in serials)
                    {
                        var inv = await trackerDBContext.Inventory.Where(x => x.Id == item.InventoryId).FirstOrDefaultAsync();
                        var wr = await trackerDBContext.Warehouse.Where(x => x.Id == item.WarehouseId).FirstOrDefaultAsync();
                        var senior = await trackerDBContext.Sensor.Where(x => x.Serial == item.Serial).FirstOrDefaultAsync();


                        var emails = item.ToEmails.Split(",");
                        if (emails != null)
                        {
                            bool result = false;
                            foreach (var email in emails)
                            {
                                result = mailService.SendEmailIsNotActive(item.ToEmails, senior.Name,inv.Name,wr.Name);
                                if (!result)
                                {
                                    Console.WriteLine("SMTP ERROR Cant send to this email " + email);
                                }
                                else
                                {
                                    Console.WriteLine("Done");
                                }
                            }

                        }
                        else
                        {
                            Console.WriteLine("Email should not be Null");
                        }

                    }
                }
                else
                {
                    Console.WriteLine("No Thing Found Not Active");
                }


                var serialsLowVoltage = await _processBackground.GetInventoryHistoryIsLowVoltage();
                if (serialsLowVoltage.Count != 0)
                {
                    foreach (var item in serialsLowVoltage)
                    {
                        var inv = await trackerDBContext.Inventory.Where(x => x.Id == item.InventoryId).FirstOrDefaultAsync();
                        var wr = await trackerDBContext.Warehouse.Where(x => x.Id == item.WarehouseId).FirstOrDefaultAsync();
                        var senior = await trackerDBContext.Sensor.Where(x => x.Serial == item.Serial).FirstOrDefaultAsync();


                        var emails = item.ToEmails.Split(",");
                        if (emails != null)
                        {
                            bool result = false;
                            foreach (var email in emails)
                            {
                                result = mailService.SendEmailIsLowVoltage(item.ToEmails, senior.Name, inv.Name, wr.Name);
                                if (!result)
                                {
                                    Console.WriteLine("SMTP ERROR Cant send to this email " + email);
                                }
                                else
                                {
                                    Console.WriteLine("Done");
                                }
                            }

                        }
                        else
                        {
                            Console.WriteLine("Email should not be Null");
                        }

                    }
                }
                else
                {
                    Console.WriteLine("No Thing Found Low Voltage");
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
            timer10M?.Dispose();
            timer1Day?.Dispose();
        }
    }
}
