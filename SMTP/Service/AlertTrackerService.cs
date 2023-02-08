using Microsoft.EntityFrameworkCore;
using SMTP.Models;
using SMTP.OtherModels;
using SMTP.ThirdModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMTP.Service
{
    public class AlertTrackerService
    {
        private readonly SMTPTrackerContext context;
        private readonly TrackerHistoryDBContext trackerHistoryDBContext;
        private readonly TrackerDBContext trackerDBContext;

        public AlertTrackerService(SMTPTrackerContext context, TrackerHistoryDBContext trackerHistoryDBContext, TrackerDBContext trackerDBContext)
        {
            this.context = context;
            this.trackerHistoryDBContext = trackerHistoryDBContext;
            this.trackerDBContext = trackerDBContext;
        }
        public async Task AlertTrackerInsertData()
        {
            var sensers = await context.AlertBySensor.ToListAsync();
            List<AlertTracker> tracks = new List<AlertTracker>();
            foreach (var item in sensers)
            {
                var treackerHistory = await trackerHistoryDBContext.InventoryHistory.Where(x =>
                x.GpsDate < DateTime.Now && x.GpsDate > DateTime.Now.AddMinutes(Convert.ToDouble(-60)) && x.Serial == item.Serial).ToListAsync();

                if (treackerHistory != null)
                {
                    var sensor = await context.AlertBySensor.Where(x => x.Serial == item.Serial).FirstOrDefaultAsync();
                    var sensorData = await context.Smtpchecker.Where(x => x.Serial == item.Serial).FirstOrDefaultAsync();

                    //inside Temperature
                    var a = treackerHistory.Where(treacker => Convert.ToDouble(treacker.Temperature) < sensor.MaxValueTemperature && Convert.ToDouble(treacker.Temperature) > sensor.MinValueTemperature).Count();
                    //OutSide Temperature
                    var b = treackerHistory.Where(treacker => Convert.ToDouble(treacker.Temperature) > sensor.MaxValueTemperature || Convert.ToDouble(treacker.Temperature) < sensor.MinValueTemperature).Count();
                    //inside Humidity
                    var c = treackerHistory.Where(treacker => Convert.ToDouble(treacker.Humidity) < sensor.MaxValueHumidity && Convert.ToDouble(treacker.Humidity) > sensor.MinValueHumidity).Count();
                    //OutSide Humidity
                    var e = treackerHistory.Where(treacker => Convert.ToDouble(treacker.Humidity) > sensor.MaxValueHumidity || Convert.ToDouble(treacker.Humidity) < sensor.MinValueHumidity).Count();
                    foreach (var treacker in treackerHistory)
                    {
                        if (a != 0 && sensorData.IsSendTemperature == true)
                        {

                                sensorData.IsSendTemperature = false;
                                sensorData.IsSendTemperatureSecond = false;
                                sensorData.UpdatedDate = treacker.GpsDate;
                                var newSensorData = context.Smtpchecker.Update(sensorData);
                                await context.SaveChangesAsync();
                        }
                        else if (b != 0 && a == 0 && sensorData.IsSendTemperature == false && sensorData.IsSendTemperatureSecond == false)
                        {
                            //OutSide Temperature
                            if (Convert.ToDouble(treacker.Temperature) > sensor.MaxValueTemperature || Convert.ToDouble(treacker.Temperature) < sensor.MinValueTemperature)
                            {
                                AlertTracker alertTracker = new AlertTracker()
                                {
                                    UserName= sensor.UserName,
                                    AlertDateTime=treacker.GpsDate,
                                    AlertType="2",
                                    MonitoredUnit= trackerDBContext.Sensor.Where(x=>x.Serial==treacker.Serial).FirstOrDefault().Name + " ("+treacker.Serial +" )",
                                    MessageForValue = "Temperature " + treacker.Temperature + " &#8451;",
                                    Serial = treacker.Serial,
                                    Zone = trackerDBContext.Inventory.Include(x=>x.Warehouse).ThenInclude(x=>x.Fleet).Where(x=>x.Id==treacker.InventoryId).FirstOrDefault().Warehouse.Fleet.NameEn,
                                    WarehouseName = trackerDBContext.Warehouse.Where(x => x.Id == sensor.WarehouseId).FirstOrDefault().Name,
                                    SendTo = sensor.ToEmails,
                                    Interval = 60,
                                    IsSend=false,
                                    AlertId=Convert.ToInt32(treacker.Id)
                                };
                                var aa = await context.AlertTracker.AddAsync(alertTracker);

                                sensorData.IsSendTemperature = true;
                                sensorData.UpdatedDate = treacker.GpsDate;
                                var newSensorData = context.Smtpchecker.Update(sensorData);

                                await context.SaveChangesAsync();
                            }
                        }
                        else if (b != 0 && a == 0 && sensorData.IsSendTemperature == true && sensorData.IsSendTemperatureSecond == false && DateTime.Now.TimeOfDay > sensorData.UpdatedDate.Value.AddMinutes(30).TimeOfDay)
                        {
                            //OutSide Temperature
                            if (Convert.ToDouble(treacker.Temperature) > sensor.MaxValueTemperature || Convert.ToDouble(treacker.Temperature) < sensor.MinValueTemperature)
                            {
                                AlertTracker alertTracker = new AlertTracker()
                                {
                                    UserName = sensor.UserName,
                                    AlertDateTime = treacker.GpsDate,
                                    AlertType = "2",
                                    MonitoredUnit = trackerDBContext.Sensor.Where(x => x.Serial == treacker.Serial).FirstOrDefault().Name + " (" + treacker.Serial + " )",
                                    MessageForValue = "Temperature " + treacker.Temperature + " &#8451;",
                                    Serial = treacker.Serial,
                                    Zone = trackerDBContext.Inventory.Include(x => x.Warehouse).ThenInclude(x => x.Fleet).Where(x => x.Id == treacker.InventoryId).FirstOrDefault().Warehouse.Fleet.NameEn,
                                    WarehouseName = trackerDBContext.Warehouse.Where(x => x.Id == sensor.WarehouseId).FirstOrDefault().Name,
                                    SendTo = sensor.ToEmails,
                                    Interval = 60,
                                    IsSend = false,
                                    AlertId = Convert.ToInt32(treacker.Id)
                                };
                                var aa = await context.AlertTracker.AddAsync(alertTracker);

                                sensorData.IsSendTemperatureSecond = true;
                                sensorData.UpdatedDate = treacker.GpsDate;
                                var newSensorData = context.Smtpchecker.Update(sensorData);

                                await context.SaveChangesAsync();
                            }
                        }
                        if (c != 0 && sensorData.IsSendHumidity == true)
                        {

                                sensorData.IsSendHumidity = false;
                                sensorData.IsSendHumiditySecond = false;
                                sensorData.UpdatedDate = treacker.GpsDate;
                                var newSensorData = context.Smtpchecker.Update(sensorData);
                                await context.SaveChangesAsync();
                            
                        }
                        else if (e != 0 && c == 0 && sensorData.IsSendHumidity == false && sensorData.IsSendHumiditySecond == false)
                        {
                            //OutSide Humidity
                            if (Convert.ToDouble(treacker.Humidity) > sensor.MaxValueHumidity || Convert.ToDouble(treacker.Humidity) < sensor.MinValueHumidity)
                            {
                                AlertTracker alertTracker = new AlertTracker()
                                {
                                    UserName = sensor.UserName,
                                    AlertDateTime = treacker.GpsDate,
                                    AlertType = "2",
                                    MonitoredUnit = trackerDBContext.Sensor.Where(x => x.Serial == treacker.Serial).FirstOrDefault().Name + " (" + treacker.Serial + " )",
                                    MessageForValue = "Humidity " + treacker.Temperature,
                                    Serial = treacker.Serial,
                                    Zone = trackerDBContext.Inventory.Include(x => x.Warehouse).ThenInclude(x => x.Fleet).Where(x => x.Id == treacker.InventoryId).FirstOrDefault().Warehouse.Fleet.NameEn,
                                    WarehouseName = trackerDBContext.Warehouse.Where(x => x.Id == sensor.WarehouseId).FirstOrDefault().Name,
                                    SendTo = sensor.ToEmails,
                                    Interval = 60,
                                    IsSend = false,
                                    AlertId = Convert.ToInt32(treacker.Id)
                                };
                                var aa = await context.AlertTracker.AddAsync(alertTracker);

                                sensorData.IsSendHumidity = true;
                                sensorData.UpdatedDate = treacker.GpsDate;
                                var newSensorData = context.Smtpchecker.Update(sensorData);

                                await context.SaveChangesAsync();
                            }
                        }
                        else if (b != 0 && a == 0 && sensorData.IsSendHumidity == true && sensorData.IsSendHumiditySecond == false && DateTime.Now.TimeOfDay > sensorData.UpdatedDate.Value.AddMinutes(30).TimeOfDay)
                        {
                            //OutSide Humidity
                            if (Convert.ToDouble(treacker.Humidity) > sensor.MaxValueHumidity || Convert.ToDouble(treacker.Humidity) < sensor.MinValueHumidity)
                            {
                                AlertTracker alertTracker = new AlertTracker()
                                {
                                    UserName = sensor.UserName,
                                    AlertDateTime = treacker.GpsDate,
                                    AlertType = "2",
                                    MonitoredUnit = trackerDBContext.Sensor.Where(x => x.Serial == treacker.Serial).FirstOrDefault().Name + " (" + treacker.Serial + " )",
                                    MessageForValue = "Humidity " + treacker.Temperature,
                                    Serial = treacker.Serial,
                                    Zone = trackerDBContext.Inventory.Include(x => x.Warehouse).ThenInclude(x => x.Fleet).Where(x => x.Id == treacker.InventoryId).FirstOrDefault().Warehouse.Fleet.NameEn,
                                    WarehouseName = trackerDBContext.Warehouse.Where(x => x.Id == sensor.WarehouseId).FirstOrDefault().Name,
                                    SendTo = sensor.ToEmails,
                                    Interval = 60,
                                    IsSend = false,
                                    AlertId = Convert.ToInt32(treacker.Id)
                                };
                                var aa = await context.AlertTracker.AddAsync(alertTracker);

                                sensorData.IsSendHumiditySecond = true;
                                sensorData.UpdatedDate = treacker.GpsDate;
                                var newSensorData = context.Smtpchecker.Update(sensorData);

                                await context.SaveChangesAsync();
                            }
                        }
                    }
                }
            }
        }
        public async Task<List<AlertTracker>> GetAlertTrackersNotTrackedAsync()
        {

            //var sensers = await context.AlertBySensor.ToListAsync();
            //List<AlertTracker> tracks = new List<AlertTracker>();
            //foreach (var item in sensers)
            //{
            //    var x = await context.AlertTracker.Where(x => x.IsSend == false && x.AlertDateTime.HasValue &&
            //                        x.AlertDateTime.Value.Date == DateTime.Today.Date && x.Serial == item.Serial).ToListAsync();

            //    var y = x.Where(x => x.AlertDateTime.Value.AddMinutes(Convert.ToDouble(30)) < DateTime.Now).OrderByDescending(x => x.AlertDateTime).FirstOrDefault();
            //    if (y != null)
            //    {
            //        tracks.Add(y);
            //    }
            //}
            return await context.AlertTracker.Where(x => x.IsSend == false).ToListAsync();

        }
        public async Task<AlertTracker> UpdateAlertTrackerToSendAsync(int id)
        {
            var alertTracker = await context.AlertTracker.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (alertTracker != null)
            {
                alertTracker.IsSend = true;
                var result = context.AlertTracker.Update(alertTracker).Entity;
                await context.SaveChangesAsync();
                return result;
            }
            else
            {
                return null;
            }
        }
        public async Task<List<AlertBySensor>> GetInventoryHistoryNotActive()
        {
            var a = await context.AlertBySensor.ToListAsync();
            List<AlertBySensor> listOfSerials = new List<AlertBySensor>();

            foreach (var item in a)
            {
                var trackerCount = await trackerHistoryDBContext.InventoryHistory.Where(x => x.GpsDate.Date == DateTime.Today.Date && x.Serial == item.Serial).CountAsync();
                if (trackerCount == 0)
                {
                    listOfSerials.Add(item);
                }
            }
            return listOfSerials;
        }
        public async Task<List<AlertBySensor>> GetInventoryHistoryIsLowVoltage()
        {
            List<AlertBySensor> alertBySensors = new List<AlertBySensor>();
            var InventoryHistory = await trackerHistoryDBContext.InventoryHistory.Where(x => x.IsLowVoltage == true && x.GpsDate.Date == DateTime.Today.Date).ToListAsync();
            foreach (var item in InventoryHistory)
            {
                alertBySensors.Add(await context.AlertBySensor.Where(x => x.Serial == item.Serial).FirstOrDefaultAsync());
            }
            return alertBySensors;

        }
        public async Task<List<Smtpsetting>> GetSmtpsettings()
        {
            return await context.Smtpsetting.Where(X => X.CurrentEmailNumber < 100).ToListAsync();
        }
        public async Task<Smtpsetting> UpdateSmtpsettingsCountAsync(int id)
        {
            var smtpsetting = await context.Smtpsetting.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (smtpsetting != null)
            {
                smtpsetting.CurrentEmailNumber = smtpsetting.CurrentEmailNumber + 1;
                var result = context.Smtpsetting.Update(smtpsetting).Entity;
                await context.SaveChangesAsync();
                return result;
            }
            else
            {
                return null;
            }
        }
        public async Task UpdateSmtpsettingsCountAsync()
        {
            var smtpsetting = await context.Smtpsetting.ToListAsync();
            if (smtpsetting != null)
            {
                foreach (var item in smtpsetting)
                {
                    item.CurrentEmailNumber = 0;
                    context.Smtpsetting.Update(item);
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
