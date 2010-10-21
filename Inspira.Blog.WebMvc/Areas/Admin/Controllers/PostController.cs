using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inspira.Blog.WebMvc.Areas.Admin.ViewModels.Post;
using Inspira.Blog.DomainModel;
using Web.Generics.ApplicationServices.DataAccess;
using Web.Generics.Infrastructure.DataAccess.NHibernate;
using Web.Generics.UserInterface.Extensions;
using Web.Generics.UserInterface.Components;

namespace Inspira.Blog.WebMvc.Areas.Admin.Controllers
{
	public class PostController : Controller
	{
		private readonly GenericRepository<Post> postRepository;
		private readonly GenericRepository<WebLog> blogRepository;
		public PostController(GenericRepository<Post> postRepository, GenericRepository<WebLog> blogRepository)
		{
			this.postRepository = postRepository;
			this.blogRepository = blogRepository;
		}

		public ActionResult Index(IndexViewModel viewModel)
		{
			FillDropDowns(viewModel);

			var expression = viewModel.Filter.GetExpression();
			var gridBuilder = new GridBuilder(viewModel.AllPosts);

			var posts = postRepository.Where(expression).ToList();

			gridBuilder.Populate(posts);

			return View(viewModel);
		}

		private void FillDropDowns(IndexViewModel viewModel)
		{
			viewModel.Filter.PublishedSelectList = new[] {
				new SelectListItem { Value = "1", Text = "Yes" }
				,new SelectListItem { Value = "2", Text = "No" }
			};

			var list = blogRepository.ToList();
			viewModel.Filter.BlogSelectList = list.ToSelectList(b => b.ID, b => b.Title);
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
