using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NNI.HedisCms.Domain.Entities;
using System.Data.Entity;

namespace NNI.HedisCms.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
    }
}
