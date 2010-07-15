using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inspira.Blog.DomainModel;
using System.Web.Mvc;
using Web.Generics.Web.Mvc;
using Web.Generics.UserInterface;

namespace Web.Generics.Tests
{
    public class PostController : GenericController<Post, PostViewModel>
    {
        public PostController(PostService service) : base(service)
        {
        }
    }
}
