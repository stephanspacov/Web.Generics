using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inspira.Blog.WebMvc.ViewModels.Posts
{
    public class IndexViewModel
    {
        public IndexViewModel()
		{
			this.PostsPublicados = new List<Inspira.Blog.DomainModel.Post>();
		}
        public List<Inspira.Blog.DomainModel.Post> PostsPublicados { get; set; }
    }
}