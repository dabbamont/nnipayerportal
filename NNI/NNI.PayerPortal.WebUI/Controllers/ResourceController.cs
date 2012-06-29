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
    [Authorize]
    public class ResourceController : Controller
    {
        // Set Paging
        public int PageSize = 9;
        public int PageSizeForVideo = 4;

        // Allow Ninject to inject the dependency for the Resource repository when it instantiates the controller class.
        private IResourceRepository repository;
        private IMyLibraryRepository nullLibraryRepository;
        private IMyLibraryRepository libraryRepository;

        public ResourceController(IResourceRepository resourceRepository)
        {
            repository = resourceRepository;
            MyLibraryController lib = new MyLibraryController(nullLibraryRepository); 
            libraryRepository = lib.GetLibraryRepository();
        }

        //
        // GET: /Resource/
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // 
        // GET: /Resource/TopPicks/
        [HttpGet]
        [ChildActionOnly]
        public ActionResult TopPicks()
        {
            if (Request.IsAjaxRequest())
            {
                IEnumerable<TopPicksViewModel> model = (from r in repository.Resources
                                                       orderby r.PayerPicks descending
                                                       select new TopPicksViewModel
                                                       {
                                                           ResourceId = r.ResourceId,
                                                           Title = r.Title,
                                                       }).Take(5);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return PartialView();
            }
        }

        //
        // GET: /Resource/MyLibrary/?sortBy=Date&sortDirection=Descending&page=1
        [HttpGet]
        public ActionResult MyLibrary(string sortBy, string sortDirection, int page = 1)
        {
            if (Request.IsAjaxRequest())
            {
                // Identify User 
                MembershipUser currentUser = Membership.GetUser();
                // Get Items
                var libraries = (from r in repository.Resources
                                 from l in libraryRepository.MyLibraries
                                 where r.ResourceId == l.ResourceId
                                 where l.UserId.ToString() == currentUser.ProviderUserKey.ToString()                                 
                                 select new MyLibraryItemViewModel
                                 {
                                     ResourceId = r.ResourceId,
                                     ThumbnailUrl = r.ThumbnailUrl
                                 })
                                 .Skip((page - 1) * PageSize)
                                 .Take(PageSize);
                // Sort
                if (sortBy == "Title")
                {
                    if (sortDirection == "Ascending")
                    {
                        libraries.OrderBy(l => l.Title);
                    }
                    else if (sortDirection == "Descending")
                    {
                        libraries.OrderByDescending(l => l.Title);
                    }
                }
                else if (sortBy == "Date")
                {
                    if (sortDirection == "Ascending")
                    {
                        libraries.OrderBy(l => l.CreatedDate);
                    }
                    else if (sortDirection == "Descending")
                    {
                        libraries.OrderByDescending(l => l.CreatedDate);
                    }
                }
                 
                MyLibraryViewModel model = new MyLibraryViewModel
                {
                    Items = libraries,
                    SortBy = sortBy,
                    SortDirection = sortDirection,
                    PagingInfo =
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = libraries.Count()                                    
                    }
                };

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View();
            }
        }

        //
        // GET: /Resource/MyLibraryItem/?resourceId=1
        [HttpGet]
        public ActionResult MyLibraryItem(int resourceId)
        {
            if (Request.IsAjaxRequest())
            {
                // Identify User
                MembershipUser currentUser = Membership.GetUser();

                // Get Item Details
                MyLibraryItemDetailsViewModel model = (from r in repository.Resources
                                                       from l in libraryRepository.MyLibraries
                                                       where r.ResourceId == resourceId
                                                       where r.ResourceId == l.ResourceId
                                                       where l.UserId.ToString() == currentUser.ProviderUserKey.ToString()
                                                       select new MyLibraryItemDetailsViewModel
                                                       {
                                                           ResourceId = r.ResourceId,
                                                           Title = r.Title,
                                                           Description = r.Description,
                                                           Category = r.Category,
                                                           ThumbnailUrl = r.ThumbnailUrl,
                                                           ResourceUrl = r.ResourceUrl
                                                       }).FirstOrDefault();

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View();
            }
        }

        // 
        // POST: /Resource/SaveMyLibrary/?resourceId=1
        [HttpPost]
        public ActionResult SaveMyLibrary(int resourceId)
        {
            if (Request.IsAjaxRequest())
            {
                // Identify User
                MembershipUser currentUser = Membership.GetUser();
                Guid userId = new Guid(currentUser.ProviderUserKey.ToString());

                MyLibrary library = new MyLibrary
                {
                    ResourceId = resourceId,
                    UserId = userId
                };
                libraryRepository.SaveLibrary(library);

                return View();
            }
            else
            {
                return View();
            }

        }

        // 
        // POST: /Resource/RemoveMyLibrary/?resourceId=1
        [HttpPost]
        public ActionResult RemoveMyLibrary(int resourceId)
        {
            if (Request.IsAjaxRequest())
            {
                // Identify User
                MembershipUser currentUser = Membership.GetUser();
                Guid userId = new Guid(currentUser.ProviderUserKey.ToString());

                MyLibrary library = new MyLibrary
                {
                    ResourceId = resourceId,
                    UserId = userId
                };
                libraryRepository.DeleteLibrary(library);

                // TODO: Return error
                return View();
            }
            else
            {
                return View();
            }

        }

        //
        // GET: /Resource/Video/?page=1
        [HttpGet]
        public ActionResult Video(int page = 1)
        {
            if (Request.IsAjaxRequest())
            {
                // Get Videos
                var videos = (from v in repository.Resources
                                 where v.ResourceMimeType == "video/mp4"
                                 select new VideoItemViewModel
                                 {
                                    ResourceId = v.ResourceId,
                                    Title = v.Title,
                                    Description = v.Description,
                                    ResourceUrl = v.ResourceUrl,
                                    ResourceMimeType = v.ResourceMimeType,
                                    ThumbnailUrl = v.ThumbnailUrl
                                 })
                                 .Skip((page - 1) * PageSizeForVideo)
                                 .Take(PageSizeForVideo);

                VideoModel model = new VideoModel
                {
                    Items = videos,
                    PagingInfo =
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSizeForVideo,
                        TotalItems = videos.Count()
                    }
                };

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View();
            }
        }

        

        //
        // GET: /Resource/Search/

        public ActionResult Search()
        {
            return View();
        }

        //
        // POST: /Resource/Search/?SearchTerm=Bob
        [HttpPost]
        public ActionResult Search(string model)
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

        //
        // GET: /Resource/List/?category=Medicare&page=1
        [HttpGet]
        public ActionResult List(string category, int page = 1)
        {
            if (Request.IsAjaxRequest())
            {
                // Get Items
                var resources = (from r in repository.Resources
                                 where r.Category == category
                                 select new ResourceItemViewModel
                                 {                                    
                                     ResourceId = r.ResourceId,
                                     ThumbnailUrl = r.ThumbnailUrl
                                 })
                                 .Skip((page - 1) * PageSize)
                                 .Take(PageSize);

                ResourceViewModel model = new ResourceViewModel
                {
                    Items = resources,
                    Category = category,
                    PagingInfo =
                    {
                        CurrentPage = page,
                        ItemsPerPage = PageSize,
                        TotalItems = resources.Count()
                    }
                };

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View();
            }
        }

        //
        // GET: /Resource/ListItem/?resourceId=1
        [HttpGet]
        public ActionResult ListItem(int resourceId)
        {
            if (Request.IsAjaxRequest())
            {
                // Get Item Details
                ResourceItemDetailsViewModel model = (from r in repository.Resources
                                                        where r.ResourceId == resourceId
                                                        select new ResourceItemDetailsViewModel
                                                        {
                                                            ResourceId = r.ResourceId,
                                                            Title = r.Title,
                                                            Description = r.Description,
                                                            Category = r.Category,
                                                            ThumbnailUrl = r.ThumbnailUrl,
                                                            ResourceUrl = r.ResourceUrl
                                                        }).FirstOrDefault();

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View();
            }
        }

        // 
        // GET: /Resource/RequestResource/
        [HttpGet]
        public ActionResult RequestResource()
        {
            return View();
        }

        // POST: /Resource/RequestResource/
        [HttpPost]
        public ActionResult RequestResource(RequestResourceModel model)
        {
            if (ModelState.IsValid && Request.IsAjaxRequest())
            {
                // Identify User
                MembershipUser currentUser = Membership.GetUser();
                try
                {
                    // Find Resource
                    Resource request = (from r in repository.Resources
                                               where r.ResourceId == model.ResourceId
                                               select r).FirstOrDefault();

                    // Build Message
                    StringBuilder mBody = new StringBuilder();
                    mBody.AppendLine("Description: NovoPayerLink - New Resource Request from " + HttpContext.Profile["FirstName"] + " " + HttpContext.Profile["LastName"]);
                    mBody.AppendLine("-- Message Details --");
                    mBody.AppendLine("First Name:      " + HttpContext.Profile["FirstName"]);
                    mBody.AppendLine("Last Name:       " + HttpContext.Profile["LastName"]);
                    mBody.AppendLine("Order Resource:  " + model.OrderResource);
                    mBody.AppendLine("Request Meeting: " + model.RequestMeeting);
                    mBody.AppendLine("Use Email:       " + model.UseEmail);
                    mBody.AppendLine("Email:           " + model.Email);
                    mBody.AppendLine("Use Phone        " + model.UsePhone);
                    mBody.AppendLine("Phone:           " + model.Phone);
                    mBody.AppendLine("Contact Time:    " + model.ContactTime);
                    mBody.AppendLine("Requested On:    " + DateTime.Now.ToShortDateString());
                    mBody.AppendLine("-- Resource Details --");
                    mBody.AppendLine("Category:        " + request.Category);
                    mBody.AppendLine("Title:           " + request.Title);
                    mBody.AppendLine("Description:     " + request.Description);
                    
                    // Email resource request to the administrators 
                    EmailMessage message = new EmailMessage
                    {
                        MailToAddress = SystemSettings.EmailNotificationsTo,
                        MailFromAddress = SystemSettings.EmailNotificationsFrom,
                        MessageSubject = "NovoPayerLink - New Resource Request from " + HttpContext.Profile["FirstName"] + " " + HttpContext.Profile["LastName"],
                        MessageBody = mBody.ToString()
                    };

                    SendMail sendMail = new SendMail(message);
                    sendMail.Send();

                    model.Error = "";
                }
                catch
                {
                    model.Error = "The information you entered is incorrect. Please try again.";
                }


                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View();
            }
        }

        #region CRUD
        public ViewResult Create()
        {
            return View("Edit", new Resource());
        }

        public ViewResult Edit(int resourceId)
        {
            Resource resource = repository.Resources.FirstOrDefault(r => r.ResourceId == resourceId);
            return View(resource);
        }

        [HttpPost]
        public ActionResult Delete(int resourceId)
        {
            Resource resource = repository.Resources.FirstOrDefault(r => r.ResourceId == resourceId);
            if (resource != null)
            {
                repository.DeleteResource(resource);
                TempData["message"] = string.Format("{0} was deleted", resource.ResourceName);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Resource resource)
        {
            if (ModelState.IsValid)
            {
                // save the resource
                repository.SaveResource(resource);
                // add a message to the viewbag
                TempData["message"] = string.Format("{0} has been saved", resource.ResourceName);

                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(resource);
            }
        }
        #endregion
    }
}
