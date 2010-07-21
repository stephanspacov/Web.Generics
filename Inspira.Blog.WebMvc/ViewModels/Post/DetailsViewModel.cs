using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inspira.Blog.DomainModel;

namespace Inspira.Blog.WebMvc.ViewModels.Posts
{
    public class DetailsViewModel
    {
        public Post Post { get; set; }

        public DetailsViewModel()
        {
            this.Post = new Post();

        }

    }
}