﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inspira.Blog.DomainModel;
using System.Web.Mvc;

namespace Web.Generics.Tests
{
    public class PostController : GenericController<Post, PostViewModel>
    {
        public PostController(PostService service) : base(service)
        {
        }
    }
}