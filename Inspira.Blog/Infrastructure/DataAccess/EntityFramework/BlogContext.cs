using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Inspira.Blog.Infrastructure.DataAccess.EntityFramework
{
    public class BlogContext : ObjectContext
    {
        public BlogContext() : base("name=Inspira_Blog", "BlogModelContainer")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            this.ContextOptions.ProxyCreationEnabled = true;
        }
    }
}
