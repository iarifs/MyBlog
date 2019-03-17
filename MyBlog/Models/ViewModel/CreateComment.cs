using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBlog.Models.ViewModel
{
    public class CreateComment
    {
        [Required]
        public string CommentBody { get; set; }

        public DateTime CommentCreated { get; set; }

        public DateTime? CommentUpdates { get; set; }
        
        public string UserName { get; set; }
        public CreateComment()
        {
            CommentCreated = DateTime.Now;
        }
    }
}