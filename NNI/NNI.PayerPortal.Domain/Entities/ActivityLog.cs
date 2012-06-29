using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NNI.PayerPortal.Domain.Entities
{
    public class ActivityLog
    {
        // Properties
        [HiddenInput(DisplayValue = false)]
        public int ActivityLogId { get; set; }
        
        public string Username { get; set; }
        public string UserType { get; set; } // All Users, Super Admins, AEs, Clients
        public string Activity { get; set; }
        public string ActivityType { get; set; } // Download, Viewed, PlacedInLibrary, Added, Deleted, Published, Session, Meeting Request
        
        [HiddenInput(DisplayValue = false)]
        public bool IsContent { get; set; }
        public string ContentTitle { get; set; }
        public string ContentType { get; set; }
        public bool IsContentPublished { get; set; }
        public DateTime ContentCreatedDate { get; set; }
        public DateTime ContentCreatedUtcDate { get; set; }
        public Guid? ContentCreatedBy { get; set; }

        // Audit
        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedDate { get; set; }
        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedUtcDate { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string CreatedBy { get; set; }
        [HiddenInput(DisplayValue = false)]
        public Guid? CreatedById { get; set; }
        [HiddenInput(DisplayValue = false)]
        public DateTime ModifiedDate { get; set; }
        [HiddenInput(DisplayValue = false)]
        public DateTime ModifiedUtcDate { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ModifiedBy { get; set; }
        [HiddenInput(DisplayValue = false)]
        public Guid? ModifiedById { get; set; }
    }
}
