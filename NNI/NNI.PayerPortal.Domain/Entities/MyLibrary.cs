using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace NNI.PayerPortal.Domain.Entities
{
    public class MyLibrary
    {
        // Properties
        [HiddenInput(DisplayValue = false)]
        public int MyLibraryId { get; set; }

        [HiddenInput(DisplayValue = false)]
        virtual public int ResourceId { get; set; }

        [HiddenInput(DisplayValue = false)]
        virtual public Guid UserId { get; set; }
    }
}
