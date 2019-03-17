using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBlog.Models.ViewModel
{
    public class PostDetails
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string AuthorName { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public string MediaUrl { get; set; }
        public string Slug { get; set; }

        [Required(ErrorMessage = "Please give some comment")]
        public string CommentBody { get; set; }

        [Required(ErrorMessage = "Update comment needed")]
        public string UpdatedComment { get; set; }

        [Required(ErrorMessage = "Reason Needed")]
        public string UpdateReason { get; set; }

        public List<CommentDetails> Comments { get; set; }

        public PostDetails()
        {
            Comments = new List<CommentDetails>();
        }
    }
}