using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inspira.Blog.WebMvc.ViewModels.Post;
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
			viewModel.PostsPublicados.Add(new Post { PublishedAt = DateTime.Now, Title = "Título do Post", Text = "bla bla bla bla" + Environment.NewLine + " bla bla bla bla bla" });

			return View(viewModel);
        }

    }
}
