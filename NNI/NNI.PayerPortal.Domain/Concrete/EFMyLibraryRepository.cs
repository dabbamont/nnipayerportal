using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NNI.PayerPortal.Domain.Abstract;
using NNI.PayerPortal.Domain.Entities;
using System.Data.Entity;

namespace NNI.PayerPortal.Domain.Concrete
{
    public class EFMyLibraryRepository : IMyLibraryRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<MyLibrary> MyLibraries
        {
            get
            {
                return context.MyLibraries;
            }
        }

        public void SaveLibrary(MyLibrary myLibrary)
        {
            if (myLibrary.MyLibraryId == 0)
            {
                context.MyLibraries.Add(myLibrary);
            }
            context.SaveChanges();
        }

        public void DeleteLibrary(MyLibrary myLibrary)
        {            
            try
            {
                // Duplicate Check
                var libraries =  from l in context.MyLibraries
                                 where l.ResourceId == myLibrary.ResourceId
                                 where l.UserId == myLibrary.UserId
                                 select l;
                foreach (var l in libraries)
                {
                    context.MyLibraries.Remove(l);
                }
                
                context.SaveChanges();
            }
            catch
            {
                // Do Nothing
            }
        }
    }
}
