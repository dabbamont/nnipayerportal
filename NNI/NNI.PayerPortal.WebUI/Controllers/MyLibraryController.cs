using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NNI.PayerPortal.Domain.Abstract;
using NNI.PayerPortal.Domain.Entities;
using NNI.PayerPortal.WebUI.Models;
using System.Web.Security;
using System.Text;
using NNI.PayerPortal.Domain.Concrete;

namespace NNI.PayerPortal.WebUI.Controllers
{
    public class MyLibraryController : Controller
    {
        private IMyLibraryRepository libraryRepository;

        public MyLibraryController(IMyLibraryRepository myLibraryRepository)
        {
            libraryRepository = myLibraryRepository;
        }

        [NonAction]
        public IMyLibraryRepository GetLibraryRepository()
        {
            return libraryRepository;
        }
    }
}