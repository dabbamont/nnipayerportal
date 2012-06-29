using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using NNI.PayerPortal.Domain.Entities;

namespace NNI.PayerPortal.WebUI.Models
{
    public class RequestResourceModel 
    {
        public int ResourceId { get; set; }
        public string OrderResource { get; set; }
        public string RequestMeeting { get; set; }
        public string UseEmail { get; set; }
        public string Email { get; set; }
        public string UsePhone { get; set; }
        public string Phone { get; set; }
        public string ContactTime { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string Error { get; set; }

        public IEnumerable<string> ErrorFields { get; set; }
    }

    public class ResourceViewModel
    {
        public IEnumerable<ResourceItemViewModel> Items { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string Category { get; set; }
    }

    public class ResourceItemViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int ResourceId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ThumbnailUrl { get; set; }
    }

    public class ResourceItemDetailsViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int ResourceId { get; set; }

        [Required(ErrorMessage = "Please enter a Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter a Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please select a Category")]
        public string Category { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ThumbnailUrl { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ResourceUrl { get; set; }
    }

    public class MyLibraryViewModel
    {
        public IEnumerable<MyLibraryItemViewModel> Items { get; set; }
        public PagingInfo PagingInfo { get; set; }

        // Date || Title
        public string SortBy { get; set; }
        // Ascending || Descending
        public string SortDirection { get; set; }
    }

    public class MyLibraryItemViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int ResourceId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ThumbnailUrl { get; set; }

        public string Title { get; set; }

        public DateTime CreatedDate { get; set; }
    }

    public class MyLibraryItemDetailsViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int ResourceId { get; set; }

        [Required(ErrorMessage = "Please enter a Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter a Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please select a Category")]
        public string Category { get; set; }
        
        [HiddenInput(DisplayValue = false)]
        public string ThumbnailUrl { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ResourceUrl { get; set; }
    }

    public class VideoModel
    {
        public IEnumerable<VideoItemViewModel> Items { get; set; }
        
        public PagingInfo PagingInfo { get; set; }
    }

    public class VideoItemViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int ResourceId { get; set; }
        
        [Required(ErrorMessage = "Please enter a Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter a Description")]
        public string Description { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ResourceMimeType { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ResourceUrl { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ThumbnailUrl { get; set; }
    }

    public class TopPicksViewModel
    {
        public int ResourceId { get; set; }

        public string Title { get; set; }

        public string ForwardUrl { get; set; }
    }
}