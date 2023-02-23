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
using SMTP.Models;

namespace SMTP
{
    public class Background : IHostedService, IDisposable
    {
        private Timer timer30M;
        private Timer timer1Day;
        private Timer timer60M;

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

            timer30M = new Timer(async o =>
            {
                Console.WriteLine("Method Started at " + DateTime.Now);
                var smtpsettings = await _processBackground.GetSmtpsettings();
                if (smtpsettings.Count() != 0)
                {
                    var settingsCount = smtpsettings.Count();

                    var count = 0;
                    await _processBackground.AlertTrackerInsertData();
                    //var x = MailService.SendEmail("Khalid@compliance.com.sa");
                    var alertTrackersNotTracked = await _processBackground.GetAlertTrackersNotTrackedAsync();
                    if (alertTrackersNotTracked.Count() != 0)
                    {
                        foreach (var item in alertTrackersNotTracked)
                        {
                            var smtpsetting = smtpsettings[count];
                            var emails = item.SendTo.Split(",");
                            if (emails != null)
                            {
                                bool result = false;
                                foreach (var email in emails)
                                {
                                    result = false;
                                    result = mailService.SendEmailAlarm(email, item, smtpsetting.UserName, smtpsetting.Password, smtpsetting.MailAddress);
                                    if (!result)
                                    {
                                        Console.WriteLine("SMTP ERROR Cant send to this email " + email);
                                    }
                                }
                                if (result)
                                {
                                    count++;
                                    await _processBackground.UpdateSmtpsettingsCountAsync(smtpsetting.Id);
                                    await _processBackground.UpdateAlertTrackerToSendAsync(item.Id);
                                    Console.WriteLine("Done Send By " + smtpsetting.UserName + " For Serial " + item.Serial);
                                    result = false;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Email should not be Null");
                            }
                            if (count == settingsCount)
                            {
                                count = 0;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Thing Found");
                    }
                }
                else
                {
                    Console.WriteLine("No SMTP Settings Found");
                }



            },
           null,
           TimeSpan.Zero,
           TimeSpan.FromMinutes(10)
           );
            #region Test
            // timer60M = new Timer(async o =>
            // {
            //     await _processBackground.UpdateSmtpsettingsCountAsync();
            // },
            // null,
            // TimeSpan.FromMinutes(55),
            // TimeSpan.FromMinutes(65)
            // );
            // timer1Day = new Timer(async o =>
            // {
            //     var smtpsettings = await _processBackground.GetSmtpsettings();
            //     if (smtpsettings.Count() != 0)
            //     {
            //         var settingsCount = smtpsettings.Count();

            //         var count = 0;
            //         var serials = await _processBackground.GetInventoryHistoryNotActive();
            //         if (serials.Count != 0)
            //         {
            //             foreach (var item in serials)
            //             {
            //                 var smtpsetting = smtpsettings[count];

            //                 var inv = await trackerDBContext.Inventory.Where(x => x.Id == item.InventoryId).FirstOrDefaultAsync();
            //                 var wr = await trackerDBContext.Warehouse.Where(x => x.Id == item.WarehouseId).FirstOrDefaultAsync();
            //                 var senior = await trackerDBContext.Sensor.Where(x => x.Serial == item.Serial).FirstOrDefaultAsync();


            //                 var emails = item.ToEmails.Split(",");
            //                 if (emails != null)
            //                 {
            //                     bool result = false;
            //                     foreach (var email in emails)
            //                     {
            //                         result = mailService.SendEmailIsNotActive(item.ToEmails, senior.Name, inv.Name, wr.Name, smtpsetting.UserName, smtpsetting.Password, smtpsetting.MailAddress);
            //                         if (!result)
            //                         {
            //                             Console.WriteLine("SMTP ERROR Cant send to this email " + email);
            //                         }
            //                         else
            //                         {
            //                             count++;
            //                             result = false;
            //                             Console.WriteLine("Done Send By " + smtpsetting.UserName);
            //                         }
            //                     }

            //                 }
            //                 else
            //                 {
            //                     Console.WriteLine("Email should not be Null");
            //                 }

            //             }
            //         }
            //         else
            //         {
            //             Console.WriteLine("No Thing Found Not Active");
            //         }
            //         if (count == settingsCount)
            //         {
            //             count = 0;
            //         }
            //         var serialsLowVoltage = await _processBackground.GetInventoryHistoryIsLowVoltage();
            //         if (serialsLowVoltage.Count != 0)
            //         {
            //             foreach (var item in serialsLowVoltage)
            //             {
            //                 var inv = await trackerDBContext.Inventory.Where(x => x.Id == item.InventoryId).FirstOrDefaultAsync();
            //                 var wr = await trackerDBContext.Warehouse.Where(x => x.Id == item.WarehouseId).FirstOrDefaultAsync();
            //                 var senior = await trackerDBContext.Sensor.Where(x => x.Serial == item.Serial).FirstOrDefaultAsync();

            //                 var smtpsetting = smtpsettings[count];

            //                 var emails = item.ToEmails.Split(",");
            //                 if (emails != null)
            //                 {
            //                     bool result = false;
            //                     foreach (var email in emails)
            //                     {
            //                         result = mailService.SendEmailIsLowVoltage(item.ToEmails, senior.Name, inv.Name, wr.Name, smtpsetting.UserName, smtpsetting.Password, smtpsetting.MailAddress);
            //                         if (!result)
            //                         {
            //                             Console.WriteLine("SMTP ERROR Cant send to this email " + email);
            //                         }
            //                         else
            //                         {
            //                             count++;
            //                             Console.WriteLine("Done Send By " + smtpsetting.UserName);
            //                         }
            //                     }

            //                 }
            //                 else
            //                 {
            //                     Console.WriteLine("Email should not be Null");
            //                 }

            //             }
            //         }
            //         else
            //         {
            //             Console.WriteLine("No Thing Found Low Voltage");
            //         }
            //         if (count == settingsCount)
            //         {
            //             count = 0;
            //         }
            //     }
            //     else
            //     {
            //         Console.WriteLine("No SMTP Settings Found");
            //     }
            // },
            //null,
            //TimeSpan.FromMinutes(45),
            //TimeSpan.FromDays(1)
            //);
            #endregion
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("printing worker stoping");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer30M?.Dispose();
            timer1Day?.Dispose();
        }
    }
}
