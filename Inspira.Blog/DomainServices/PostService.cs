using System.Collections.Generic;
using System.Linq;
using Inspira.Blog.DomainModel;
using Web.Generics.ApplicationServices.DataAccess;
using Web.Generics.DomainServices;

namespace Inspira.Blog.DomainServices
{
    public class PostService : GenericService<Post>
    {
        private readonly IRepository<Post> postRepository;
        public PostService(IRepository<Post> postRepository) : base(postRepository)
        {
            this.postRepository = postRepository;
        }

        public IList<Post> SelectLastPublishedPosts(int qty)
        {
            return this.postRepository.Select(p => p.IsPublished == true).OrderByDescending(p => p.PublishedAt).Take(qty).ToList();
        }
    }
}
