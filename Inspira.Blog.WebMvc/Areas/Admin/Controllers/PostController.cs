using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inspira.Blog.WebMvc.Areas.Admin.ViewModels.Post;
using Web.Generics.UserInterface.HtmlHelpers;
using Inspira.Blog.DomainModel;
using Web.Generics.ApplicationServices.DataAccess;
using Web.Generics.Infrastructure.DataAccess.NHibernate;


namespace Inspira.Blog.WebMvc.Areas.Admin.Controllers
{
	public class PostController : Controller
	{
		private readonly GenericRepository<Post> postRepository;
		public PostController(GenericRepository<Post> postRepository)
		{
			this.postRepository = postRepository;
		}

		public ActionResult Index()
		{
			var viewModel = new IndexViewModel();
			var gridBuilder = new GridBuilder(viewModel.AllPosts);

			var posts = new[] {
				new Post { Title= "post 1" },
				new Post { Title= "post 2" },
				new Post { Title= "post 3" },
			};

			gridBuilder.Populate(posts);

			return View(viewModel);
		}

		public ActionResult Create(Int32? id)
		{
			var viewModel = new CreateViewModel();

			if (id > 0)
			{
				var post = postRepository.Where(p => p.ID == id).SingleOrDefault();
				viewModel.PostID = post.ID;
				viewModel.PostText = post.Text;
				viewModel.PostTitle = post.Title;
			}

			//			viewModel.BlogCreated = false;
			return View(viewModel);
		}

		[HttpPost]
		public ActionResult Create(String action, CreateViewModel viewModel)
		{
			if (!ModelState.IsValid) return View(viewModel);

			PostState postState;
			Post post;

			if (viewModel.PostID == 0)
			{
				postState = PostState.New;
				post = new Post();
				post.WebLog.ID = 1;
			}
			else
			{
				post = postRepository.Where(p => p.ID == viewModel.PostID).SingleOrDefault();

				if (post.IsPublished)
				{
					postState = PostState.Published;
				}
				else
				{
					postState = PostState.Draft;
				}
			}

			post.Text = viewModel.PostText;
			post.Title = viewModel.PostTitle;

			post.LastUpdatedAt = DateTime.Now;

			if (post.ID == 0) // insert
			{
				post.CreatedAt = DateTime.Now;
			}

			// action = ("Publish", "Save")
			if (action == "Publish")
			{
				post.IsPublished = true;
				post.LastUpdatedAt = DateTime.Now;

				if (post.ID == 0) // insert
				{
					post.CreatedAt = DateTime.Now;
				}
				postRepository.SaveOrUpdate(post);
				postRepository.SaveChanges();

				// redirect to confirmation
				ViewData["ID"] = post.ID;
				return View("PublishConfirm");
			}
			else // Save
			{
				post.IsPublished = false;
				postRepository.SaveOrUpdate(post);
				postRepository.SaveChanges();

				if (postState == PostState.Published)
				{
					// RedirectToPostList();
					return RedirectToAction("Index");
				}
				else
				{
					// ShowForm();

					// se criou o post, exibe mensagem de sucesso
					viewModel.BlogCreated = true;

					// passo o ID pra view no caso de nova inserção
					viewModel.PostID = post.ID;

					// exibe o form novamente
					return View(viewModel);
				}
			}			
		}

		enum PostState
		{
			New,
			Draft,
			Published
		}
	}
}
