using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Generics.DomainServices;
using Inspira.Blog.DomainModel;
using Web.Generics.Tests.Repositories;

namespace Web.Generics.Tests
{
    public class PostService : GenericService<Post>
    {
        public PostService(IPostRepository postRepository) : base(postRepository) {
        }
    }
}
