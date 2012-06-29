using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NNI.PayerPortal.Domain.Abstract;
using NNI.PayerPortal.WebUI.Models;

namespace NNI.PayerPortal.WebUI.Controllers
{
    public class NavController : Controller
    {
        // TODO: Implement
        //private IResourceRepository repository;

        //public NavController(IResourceRepository repo)
        //{
        //    repository = repo;
        //}

        //
        // GET: /Nav/

        public ViewResult Menu() // Menu (string category = null)
        {           
            // TODO: Implement
            //ViewBag.SelectedCategory = category;
            //IEnumerable<string> categories = repository.Resources
            //                        .Select(x => x.Category)
            //                        .Distinct()
            //                        .OrderBy(x => x);

            return View(); // return View(categories);
        }

    }
}
