using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NNI.HedisCms.Domain.Abstract;

namespace NNI.HedisCms.WebUI.Controllers
{
    public class PlanController : Controller
    {
        //
        // GET: /Plan/

        // Allow Ninject to inject the dependency for the Plan repository when it instantiates the controller class.
        private IPlanRepository repository;

        public PlanController(IPlanRepository PlanRepository)
        {
            repository = PlanRepository;
        }

        // A view that displays the complete list of Plans
        public ViewResult List()
        {
            return View(repository.Plans);
        }
    }
}
