using Microsoft.EntityFrameworkCore;
using SMTP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMTP.Service
{
    public class AlertTrackerService
    {
        private readonly SMTPTrackerContext context;

        public AlertTrackerService(SMTPTrackerContext context)
        {
            this.context = context;
        }
        public async Task<List<AlertTracker>> GetAlertTrackersNotTrackedAsync()
        {
            return await context.AlertTracker.Where(x=>x.IsSend==false && x.AlertDateTime.HasValue  && x.AlertDateTime.Value.Date == DateTime.Today.Date).ToListAsync();
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
    }
}
