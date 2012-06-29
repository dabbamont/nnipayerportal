using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNI.HedisCms.Domain.Entities
{
    public class Plan
    {
        // Properties
        public int PlanId { get; set; }
        public string PlanName { get; set; }
        public string State { get; set; }
        public string PlanType { get; set; }
        public string DiseaseState { get; set; }
        public string Grade { get; set; }

        // Audit
        public DateTime CreatedDate { get; set; }
        public DateTime CreatedUtcDate { get; set; }
        public string CreatedBy { get; set; }
        public Guid? CreatedById { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime ModifiedUtcDate { get; set; }
        public string ModifiedBy { get; set; }
        public Guid? ModifiedById { get; set; }
        public bool IsDeleted { get; set; }
    }
}
