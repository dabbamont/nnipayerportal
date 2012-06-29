using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NNI.PayerPortal.Domain.Abstract;
using NNI.PayerPortal.Domain.Entities;
using System.Data.Entity;

namespace NNI.PayerPortal.Domain.Concrete
{
    public class EFPlanRepository : IPlanRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Plan> Plans
        {
            get
            {
                return context.Plans;
            }
        }

        public void SavePlan(Plan plan)
        {
            if (plan.PlanId == 0)
            {
                context.Plans.Add(plan);
            }
            context.SaveChanges();
        }

        public void DeletePlan(Plan plan)
        {
            context.Plans.Remove(plan);
            context.SaveChanges();
        }
    }
}
