using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inspira.Blog.DomainModel;
using System.Web.Mvc;
using Web.Generics.Web.Mvc;
using Web.Generics.UserInterface;
using Web.Generics.Tests.Repositories;

namespace Web.Generics.Tests
{
    public class PostController : GenericController<Post, PostViewModel>
    {
		public PostController(PostRepository repository) : base(repository)
        {
        }
    }
}
