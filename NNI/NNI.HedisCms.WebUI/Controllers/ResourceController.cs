using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NNI.HedisCms.Domain.Abstract;

namespace NNI.HedisCms.WebUI.Controllers
{
    public class ResourceController : Controller
    {
        // Set Paging
        public int PageSize = 9;

        //
        // GET: /Resource/

        // Allow Ninject to inject the dependency for the Resource repository when it instantiates the controller class.
        private IResourceRepository repository;

        public ResourceController(IResourceRepository resourceRepository)
        {
            repository = resourceRepository;
        }

        // A view that displays the complete list of Resources
        public ViewResult List(int page = 1)
        {
            // Paging Logic
            return View(repository.Resources
                .OrderBy(r => r.ResourceId)
                .Skip((page - 1) * PageSize)
                .Take(PageSize));
        }
    }
}
