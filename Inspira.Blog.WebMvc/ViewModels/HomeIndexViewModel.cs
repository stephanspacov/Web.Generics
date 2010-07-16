using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inspira.Blog.DomainModel;

namespace Inspira.Blog.WebMvc.ViewModels
{
    public class HomeIndexViewModel
    {
        public IList<WebLog> WebLogs { get; set; }
        public IList<WebLog> LastWebLogs { get; set; }
        public IList<Inspira.Blog.DomainModel.Post> LastPublishedPosts { get; set; }
    }
}