using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlog.Models.ViewModel
{
    public class CommentDetails
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string Reason { get; set; }
        public string UserName { get; set; }
    }
}