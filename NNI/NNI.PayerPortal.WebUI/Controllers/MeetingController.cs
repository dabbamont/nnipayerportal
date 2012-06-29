using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc; 
using NNI.PayerPortal.WebUI.Models;

namespace NNI.PayerPortal.WebUI.Controllers
{
    public class MeetingController : Controller
    {
        //
        // GET: /Meeting/
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        //
        // POST: /Meeting/
        [HttpPost]
        public ActionResult Index(MeetingViewModel model)
        {
            if (ModelState.IsValid && Request.IsAjaxRequest())
            {
                // TODO: DO SOMETHING!!!!

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View();
            }
        }
    }
}
