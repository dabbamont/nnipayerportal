using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NNI.HedisCms.Domain.Abstract;
using NNI.HedisCms.Domain.Entities;

namespace NNI.HedisCms.Domain.Concrete
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
    }
}
