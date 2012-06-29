using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NNI.PayerPortal.Domain.Entities;
using NNI.PayerPortal.Domain.Abstract;
using NNI.PayerPortal.WebUI.Models;

namespace NNI.PayerPortal.WebUI.Controllers
{
    [Authorize]
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
        //
        // GET: /Plan/HedisSearch

        public ActionResult HedisSearch()
        {
            return View();
        }

        //
        // POST: /Plan/HedisSearch
        [HttpPost]
        public ActionResult HedisSearch(PlanViewModel model)
        {
            if (ModelState.IsValid && Request.IsAjaxRequest())
            {
                // TODO: 
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View();
            }
        }
        #region CRUD
        // A view that displays the complete list of Plans
        public ViewResult List()
        {
            return View(repository.Plans);
        }

        public ViewResult Create()
        {
            return View("Edit", new Plan());
        }

        public ViewResult Edit(int planId)
        {
            Plan plan = repository.Plans.FirstOrDefault(p => p.PlanId == planId);
            return View(plan);
        }

        [HttpPost]
        public ActionResult Delete(int planId)
        {
            Plan plan = repository.Plans.FirstOrDefault(p => p.PlanId == planId);
            if (plan != null)
            {
                repository.DeletePlan(plan);
                TempData["message"] = string.Format("{0} was deleted", plan.PlanName);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Plan plan)
        {
            if (ModelState.IsValid)
            {
                // save the plan
                repository.SavePlan(plan);
                // add a message to the viewbag
                TempData["message"] = string.Format("{0} has been saved", plan.PlanName);

                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(plan);
            }
        }
        #endregion
    }
}
