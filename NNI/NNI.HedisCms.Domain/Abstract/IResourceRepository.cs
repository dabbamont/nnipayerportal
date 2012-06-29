using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NNI.HedisCms.Domain.Entities;

namespace NNI.HedisCms.Domain.Abstract
{
    public interface IResourceRepository
    {
        IQueryable<Resource> Resources { get; }
    }
}
