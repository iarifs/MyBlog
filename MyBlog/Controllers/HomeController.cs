using MyBlog.Models;
using MyBlog.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext DbContext = new ApplicationDbContext();

        public ActionResult Index()
        {

            var model = DbContext.Posts
             .OrderByDescending(p => p.Id)
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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}