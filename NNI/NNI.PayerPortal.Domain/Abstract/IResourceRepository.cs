using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NNI.PayerPortal.Domain.Entities;

namespace NNI.PayerPortal.Domain.Abstract
{
    public interface IResourceRepository
    {
        IQueryable<Resource> Resources { get; }

        void SaveResource(Resource resource);

        void DeleteResource(Resource resource);
    }
}
