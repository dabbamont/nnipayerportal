using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NNI.HedisCms.Domain.Abstract;

namespace NNI.HedisCms.WebUI.Controllers
{
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
    }
}
