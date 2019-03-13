using Microsoft.AspNet.Identity;
using MyBlog.Models;
using MyBlog.Models.Domain;
using MyBlog.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace MyBlog.Controllers
{
    public class PostController : Controller
    {
        private ApplicationDbContext DbContext;

        public object ClientScript { get; private set; }
        public Page Page { get; private set; }

        public PostController()
        {
            DbContext = new ApplicationDbContext();
        }

        // GET: Post
        public ActionResult Index()
        {

            var model = DbContext.Posts
                .Select(p => new ListAllPostViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Body = p.Body,
                    AuthorName = p.Author,
                    MediaUrl = p.MediaUrl,
                    Created = p.DateCreated,
                    Updated = p.DateUpdated,
                }).ToList();

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(CreatePostViewModel formData)
        {
            return ContentCreator(null, formData);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {

            if (!id.HasValue)
            {
                return RedirectToAction(nameof(PostController.Index));
            }


            var post = DbContext.Posts.FirstOrDefault(p => p.Id == id);

            if (post == null)
            {
                return RedirectToAction(nameof(PostController.Index));
            }

            return View(post);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, CreatePostViewModel formData)
        {
            return ContentCreator(id, formData);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var post = DbContext.Posts.FirstOrDefault(p => p.Id == id);
            DbContext.Posts.Remove(post);
            DbContext.SaveChanges();
            return RedirectToAction(nameof(PostController.PostLists));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult PostLists()
        {
            var model = DbContext.Posts
             .Select(p => new ListAllPostViewModel
             {
                 Id = p.Id,
                 Title = p.Title,
                 Body = p.Body,
                 AuthorName = p.Author,
                 MediaUrl = p.MediaUrl,
                 Created = p.DateCreated,
             }).ToList();
            return View(model);
        }

        public ActionResult Error()
        {
            Response.StatusCode = 404;
            return View();
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {

            if (!id.HasValue)
            {
                return RedirectToAction(nameof(PostController.Error));
            }
            var post = DbContext.Posts.FirstOrDefault(p => p.Id == id);

            if (post == null)
            {
                return RedirectToAction(nameof(PostController.Error));
            }

            var model = new PostDetails();
            model.Title = post.Title;
            model.Body = post.Body;
            model.MediaUrl = post.MediaUrl;
            model.AuthorName = post.Author;
            model.Created = post.DateCreated;
            model.Updated = post.DateUpdated;

            return View(model);
        }

        //private method to handle create post and edit postSS
        private ActionResult ContentCreator(int? id, CreatePostViewModel formData)
        {
            if (ModelState.IsValid)
            {
                if (!id.HasValue)
                {
                    if (DbContext.Posts.Any(p => p.Title == formData.Title))
                    {
                        ModelState.AddModelError(nameof(Post.Title), "Post Title should be unique!");
                        return View();
                    }
                }


                Post post;

                var userId = User.Identity.GetUserId();
                //gettting the username by user identity class and get username function
                //Split our result from "@" char and take first one and Make it upper case
                var userName = User.Identity.GetUserName().Split('@')[0].ToUpper();

                if (!id.HasValue)
                {
                    post = new Post
                    {
                        UserId = userId
                    };
                    DbContext.Posts.Add(post);
                }
                else
                {
                    post = DbContext.Posts.FirstOrDefault(p => p.Id == id);
                    if (post == null)
                    {
                        return RedirectToAction(nameof(PostController.Index));
                    }
                    post.DateUpdated = DateTime.Now;
                }

                post.Title = formData.Title;
                post.Body = formData.Body;
                post.UserId = userId;
                post.Author = userName;
                post.Published = formData.Published;


                if (formData.Media != null)
                {
                    var fileName = formData.Media.FileName;
                    var fullPathWithName = Constants.MappedUploadFolder + fileName;
                    var fileExtensionName = Path.GetExtension(formData.Media.FileName);

                    if (!Constants.AllowedFileExtension.Contains(fileExtensionName))
                    {
                        ModelState.AddModelError("", fileExtensionName + " is not supported");
                        return View();
                    }

                    if (!Directory.Exists(Constants.MappedUploadFolder))
                    {
                        Directory.CreateDirectory(Constants.MappedUploadFolder);
                    }

                    if (System.IO.File.Exists(fullPathWithName))
                    {
                        System.IO.File.Delete(fullPathWithName);
                    }

                    formData.Media.SaveAs(fullPathWithName);

                    post.MediaUrl = Constants.UploadFolder + fileName;
                }

            }

            DbContext.SaveChanges();

            return RedirectToAction(nameof(PostController.Index));
        }
    }
}