using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NNI.PayerPortal.Domain.Entities;
using NNI.PayerPortal.Domain.Abstract;

namespace NNI.PayerPortal.WebUI.Controllers
{
    [Authorize]
    public class ActivityLogController : Controller
    {
        //
        // GET: /ActivityLog/

        // Allow Ninject to inject the dependency for the ActivityLog repository when it instantiates the controller class.
        private IActivityLogRepository repository;

        public ActivityLogController(IActivityLogRepository ActivityLogRepository)
        {
            repository = ActivityLogRepository;
        }

        // A view that displays the complete list of ActivityLogs
        public ViewResult List()
        {
            return View(repository.ActivityLogs);
        }

        public ViewResult Create()
        {
            return View("Edit", new ActivityLog());
        }

        public ViewResult Edit(int activitylogId)
        {
            ActivityLog activitylog = repository.ActivityLogs.FirstOrDefault(a => a.ActivityLogId == activitylogId);
            return View(activitylog);
        }

        [HttpPost]
        public ActionResult Delete(int activitylogId)
        {
            ActivityLog activitylog = repository.ActivityLogs.FirstOrDefault(a => a.ActivityLogId == activitylogId);
            if (activitylog != null)
            {
                repository.DeleteActivityLog(activitylog);
                TempData["message"] = string.Format("{0} was deleted", activitylog.Activity);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(ActivityLog activitylog)
        {
            if (ModelState.IsValid)
            {
                // save the activitylog
                repository.SaveActivityLog(activitylog);
                // add a message to the viewbag
                TempData["message"] = string.Format("{0} has been saved", activitylog.Activity);
                
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(activitylog);
            }
        }
    }
}
