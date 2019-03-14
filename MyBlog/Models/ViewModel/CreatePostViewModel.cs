using MyBlog.Models.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Models.ViewModel
{
    public class CreatePostViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        public string Body { get; set; }

        public ApplicationUser User { get; set; }
        public DateTime DateCreated { get; set; }

        public HttpPostedFileBase Media {get;set;}

        public bool Published { get; set; }

    }
}