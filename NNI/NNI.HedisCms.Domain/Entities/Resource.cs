using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNI.HedisCms.Domain.Entities
{
    public class Resource
    {
        // Properties
        public int ResourceId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool IsPublished { get; set; }
        public string ThumbnailName { get; set; }
        public string ThumbnailExt { get; set; }
        public string ThumbnailMimeType { get; set; }
        public string ThumbnailUrl { get; set; }
        public string ResourceName { get; set; }
        public string ResourceExt { get; set; }
        public string ResourceMimeType { get; set; }
        public string ResourceUrl { get; set; }
        public int ListOrder { get; set; }

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
