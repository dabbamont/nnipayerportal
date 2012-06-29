using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NNI.PayerPortal.Domain.Entities;

namespace NNI.PayerPortal.Domain.Abstract
{
    public interface IActivityLogRepository
    {
        IQueryable<ActivityLog> ActivityLogs { get; }

        void SaveActivityLog(ActivityLog activitylog);

        void DeleteActivityLog(ActivityLog activitylog);     
    }
}
