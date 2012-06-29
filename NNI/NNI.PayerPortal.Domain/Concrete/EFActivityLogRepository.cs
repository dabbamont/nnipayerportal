using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NNI.PayerPortal.Domain.Abstract;
using NNI.PayerPortal.Domain.Entities;
using System.Data.Entity;

namespace NNI.PayerPortal.Domain.Concrete
{
    public class EFActivityLogRepository : IActivityLogRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<ActivityLog> ActivityLogs
        {             
            get
            {
                return context.ActivityLogs;
            }
        }

        public void SaveActivityLog(ActivityLog activitylog)
        {
            if (activitylog.ActivityLogId == 0)
            {
                context.ActivityLogs.Add(activitylog);
            }
            context.SaveChanges();
        }

        public void DeleteActivityLog(ActivityLog activitylog)
        {
            context.ActivityLogs.Remove(activitylog);
            context.SaveChanges();
        }
    }
}
