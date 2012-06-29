using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NNI.HedisCms.Domain.Abstract;
using NNI.HedisCms.Domain.Entities;

namespace NNI.HedisCms.Domain.Concrete
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
    }
}
