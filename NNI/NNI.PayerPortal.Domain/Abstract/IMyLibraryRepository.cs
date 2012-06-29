using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NNI.PayerPortal.Domain.Entities;

namespace NNI.PayerPortal.Domain.Abstract
{
    public interface IMyLibraryRepository
    {
        IQueryable<MyLibrary> MyLibraries { get; }

        void SaveLibrary(MyLibrary myLibrary);

        void DeleteLibrary(MyLibrary myLibrary);
    }
}
