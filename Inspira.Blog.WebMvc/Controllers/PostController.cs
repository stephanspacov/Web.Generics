using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inspira.Blog.WebMvc.ViewModels.Posts;
using Inspira.Blog.DomainModel;

namespace Inspira.Blog.WebMvc.Controllers
{
    public class PostController : Controller
    {
        //
        // GET: /Post/

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

            var post = new Post();
            post.Title = "titulo" + id;
            post.Text = "texto";
            post.PublishedAt = DateTime.Now.AddDays(-3);
            viewModel.Post = post;

            return View(viewModel);
        }
    }
}
