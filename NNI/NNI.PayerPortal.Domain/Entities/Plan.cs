using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NNI.PayerPortal.Domain.Entities
{
    public class Plan
    {
        // Properties
        [HiddenInput(DisplayValue = false)]
        public int PlanId { get; set; }
        [Required(ErrorMessage = "Please enter a plan name")]
        public string PlanName { get; set; }
        [Required(ErrorMessage = "Please select a state")]
        public string State { get; set; }
        [Required(ErrorMessage = "Please select a plan type")]
        public string PlanType { get; set; }
        [Required(ErrorMessage = "Please select a disease state")]
        public string DiseaseState { get; set; }
        [Required(ErrorMessage = "Please select a plan grade")]
        public string Grade { get; set; }

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
        [HiddenInput(DisplayValue = false)]
        public bool IsDeleted { get; set; }
    }
}
