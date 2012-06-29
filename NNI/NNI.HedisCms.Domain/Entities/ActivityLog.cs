using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNI.HedisCms.Domain.Entities
{
    public class ActivityLog
    {
        // Properties
        public int ActivityLogid { get; set; }
        public string Username { get; set; }
        public string UserType { get; set; } // All Users, Super Admins, AEs, Clients
        public string Activity { get; set; }
        public string ActivityType { get; set; } // Download, Viewed, PlacedInLibrary, Added, Deleted, Published, Session, Meeting Request
        public bool IsContent { get; set; }
        public string ContentTitle { get; set; }
        public string ContentType { get; set; }
        public bool IsContentPublished { get; set; }
        public DateTime ContentCreatedDate { get; set; }
        public DateTime ContentCreatedUtcDate { get; set; }
        public string ContentCreatedBy { get; set; }

        // Audit
        public DateTime CreatedDate { get; set; }
        public DateTime CreatedUtcDate { get; set; }
        public string CreatedBy { get; set; }
        public Guid? CreatedById { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime ModifiedUtcDate { get; set; }
        public string ModifiedBy { get; set; }
        public Guid? ModifiedById { get; set; }
    }
}
