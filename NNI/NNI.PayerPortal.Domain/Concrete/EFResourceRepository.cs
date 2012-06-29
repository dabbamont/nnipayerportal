using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NNI.PayerPortal.Domain.Abstract;
using NNI.PayerPortal.Domain.Entities;
using System.Data.Entity;

namespace NNI.PayerPortal.Domain.Concrete
{
    public class EFResourceRepository : IResourceRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Resource> Resources
        {
            get
            {
                return context.Resources;
            }
        }

        public void SaveResource(Resource resource)
        {
            if (resource.ResourceId == 0)
            {
                context.SaveChanges();
            }
        }

        public void DeleteResource(Resource resource)
        {
            context.Resources.Remove(resource);
            context.SaveChanges();
        }
    }
}
