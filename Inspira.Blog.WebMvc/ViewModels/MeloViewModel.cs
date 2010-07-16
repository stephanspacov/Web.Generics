using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inspira.Blog.DomainModel;

namespace Inspira.Blog.WebMvc.ViewModels
{
    public class MeloViewModel
    {
        public Boolean VeioDoFuturo { get; set; }
        public String Anos { get; set; }
        public String Nome { get; set; }
        IList<Inspira.Blog.DomainModel.Post> Posts { get; set; }
    }
}