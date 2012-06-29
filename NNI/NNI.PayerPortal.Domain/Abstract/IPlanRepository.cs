using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NNI.PayerPortal.Domain.Entities;

namespace NNI.PayerPortal.Domain.Abstract
{
    public interface IPlanRepository
    {
        IQueryable<Plan> Plans { get; }

        void SavePlan(Plan plan);

        void DeletePlan(Plan plan);
    }
}
