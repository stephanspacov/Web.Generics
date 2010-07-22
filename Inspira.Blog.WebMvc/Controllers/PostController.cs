using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inspira.Blog.WebMvc.ViewModels.Posts;
using Inspira.Blog.DomainModel;
using Web.Generics.ApplicationServices.DataAccess;

namespace Inspira.Blog.WebMvc.Controllers
{
    public class PostController : Controller
    {
        //
        // GET: /Post/
        private readonly GenericRepository<WebLog> WebLogs;
        public PostController(GenericRepository<WebLog> WebLogs)

        {
            this.WebLogs = WebLogs;
        }

        public ActionResult Index()
        {
            var viewModel = new IndexViewModel();

            var post = new Post();
            post.Title = "titulo";
            post.Text = "texto";
            post.PublishedAt = DateTime.Now.AddDays(-3);

            viewModel.PostsPublicados.Add(post);

            return View(viewModel);
        }

        public ActionResult Details(Int32 id)
        {
            var viewModel = new DetailsViewModel();
            var webLog = WebLogs.Where(w => w.ID == 1).SingleOrDefault(); // TODO
            var post = webLog.Posts.Where(p => p.ID == id).SingleOrDefault();

            viewModel.Post = post;

            return View(viewModel);
        }
    }
}
