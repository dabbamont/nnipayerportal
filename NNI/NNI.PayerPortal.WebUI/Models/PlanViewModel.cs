using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NNI.PayerPortal.WebUI.Models
{
    public class PlanViewModel
    {
        IEnumerable<PlanItemViewModel> Plans { get; set; }
    }

    public class PlanItemViewModel
    {
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
    }
}