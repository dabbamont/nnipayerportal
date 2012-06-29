using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NNI.PayerPortal.Domain.Entities;

namespace NNI.PayerPortal.WebUI.Models
{
    public class ResourcesListViewModel
    {
        public IEnumerable<Resource> Resources { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}