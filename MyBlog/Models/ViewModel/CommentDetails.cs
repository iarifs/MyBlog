using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlog.Models.ViewModel
{
    public class CommentDetails
    {
        public string Body { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string User { get; set; }
    }
}