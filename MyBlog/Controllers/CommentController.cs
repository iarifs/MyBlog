using Microsoft.AspNet.Identity;
using MyBlog.Models;
using MyBlog.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Controllers
{
    public class CommentController : Controller
    {
        ApplicationDbContext DbContext;

        public CommentController()
        {
            DbContext = new ApplicationDbContext();
        }
        // GET: Comment
        public ActionResult Index(int postId)
        {
            return new HttpNotFoundResult();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return new HttpNotFoundResult();
        }

        [HttpPost]
        public ActionResult Create(string slug, CreateComment formData)
        {
            if (string.IsNullOrWhiteSpace(slug))
            {
                return new HttpNotFoundResult();
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(CommentController.Index));
            }
            Comment comment;
            comment = new Comment();
            comment.Body = formData.CommentBody;
            comment.CommentCreated = formData.CommentCreated;
            comment.Slug = slug;
            DbContext.Comments.Add(comment);
            DbContext.SaveChanges();

            return RedirectToAction("DetailsBySlug", "Post", new { slug = slug });
        }

    }
}