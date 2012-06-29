using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace NNI.PayerPortal.Domain.Entities
{
    public class Resource
    {
        // Properties
        [HiddenInput(DisplayValue = false)]
        public int ResourceId { get; set; }

        [Required(ErrorMessage = "Please enter a Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter a Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please select a Category")]
        public string Category { get; set; }
 
        public bool IsPublished { get; set; }
        public string ThumbnailName { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ThumbnailExt { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ThumbnailMimeType { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ThumbnailUrl { get; set; }

        public string ResourceName { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ResourceExt { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ResourceMimeType { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ResourceUrl { get; set; }

        public int ListOrder { get; set; }
        public int PayerPicks { get; set; }

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
