using Microsoft.EntityFrameworkCore;
using SMTP.Models;
using SMTP.OtherModels;
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

        public AlertTrackerService(SMTPTrackerContext context, TrackerHistoryDBContext trackerHistoryDBContext)
        {
            this.context = context;
            this.trackerHistoryDBContext = trackerHistoryDBContext;
        }
        public async Task<List<AlertTracker>> GetAlertTrackersNotTrackedAsync()
        {
            //var x = await context.AlertTracker.Where(x => x.IsSend == false && x.AlertDateTime.HasValue &&
            //x.AlertDateTime.Value.Date == DateTime.Today.Date && x.AlertDateTime.Value.AddMinutes(Convert.ToDouble(x.Interval)) < DateTime.Now).ToListAsync();

            return await context.AlertTracker.Where(x => x.IsSend == false && x.AlertDateTime.HasValue &&
                                x.AlertDateTime.Value.Date == DateTime.Today.Date).ToListAsync();

            //return x.Where(x => x.AlertDateTime.Value.AddMinutes(Convert.ToDouble(x.Interval)) < DateTime.Now).ToList();

            //return await context.AlertTracker.Where(x=>x.IsSend==false && x.AlertDateTime.HasValue  && 
            //x.AlertDateTime.Value.Date == DateTime.Today.Date ).ToListAsync();
        }
        public async Task<AlertTracker> UpdateAlertTrackerToSendAsync(int id)
        {
            var alertTracker = await context.AlertTracker.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (alertTracker != null)
            {
                alertTracker.IsSend= true;
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
            return await context.Smtpsetting.ToListAsync();
        }
        public async Task<Smtpsetting> UpdateSmtpsettingsCountAsync(int id)
        {
            var smtpsetting = await context.Smtpsetting.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (smtpsetting != null)
            {
                smtpsetting.CurrentEmailNumber =+1 ;
                var result = context.Smtpsetting.Update(smtpsetting).Entity;
                await context.SaveChangesAsync();
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
