using System;
using System.Web.Mvc;
using Inspira.Blog.DomainModel;
using Web.Generics.ApplicationServices.DataAccess;

namespace Inspira.Blog.WebMvc.Controllers
{
    public class TestController : Controller
    {
        private readonly GenericRepository<Post> postRepository;
        public TestController(GenericRepository<Post> postRepository)
        {
            this.postRepository = postRepository;
        }

        public ActionResult InsertPosts()
        {
            for (Int32 i = 0; i < 100; i++)
            {
                this.postRepository.SaveOrUpdate(CreateRandomPost(i));
            }
            this.postRepository.SaveChanges();
            return Content("Posts inseridos");
        }

        private Post CreateRandomPost(Int32 i)
        {
            var rnd = new Random(i * (int)DateTime.Now.Ticks);
            return new Post
            {
                CreatedAt = DateTime.Now.AddSeconds(-new Random().Next()),
                IsPublished = rnd.NextDouble() < 0.5,
                LastUpdatedAt = DateTime.Now.AddMilliseconds(-rnd.Next()),
                PublishedAt = DateTime.Now.AddMilliseconds(-rnd.Next()),
                Text = "Texto do post aleatório " + Guid.NewGuid().ToString(),
                Title = "Post aleatório " + Guid.NewGuid().ToString(),
                WebLog = new WebLog { ID = rnd.Next(5) + 1 }
            };
        }
    }
}