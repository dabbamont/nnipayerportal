using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NNI.HedisCms.Domain.Entities;

namespace NNI.HedisCms.Domain.Abstract
{
    public interface IActivityLogRepository
    {
        IQueryable<ActivityLog> ActivityLogs { get; }
    }
}
