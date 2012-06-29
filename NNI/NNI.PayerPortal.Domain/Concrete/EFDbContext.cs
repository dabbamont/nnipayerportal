using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using NNI.PayerPortal.Domain.Entities;

namespace NNI.PayerPortal.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<MyLibrary> MyLibraries { get; set; }
    }
}
