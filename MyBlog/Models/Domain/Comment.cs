using MyBlog.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlog.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime CommentCreated { get; set; }
        public DateTime? CommentUpdated { get; set; }
        public string UpdatedReason { get; set; }
        public string Slug { get; set; }
        public Post Post { get; set; }
        public virtual ApplicationUser User { get; set; }

        public Comment()
        {
            CommentCreated = DateTime.Now;
        }
    }
}