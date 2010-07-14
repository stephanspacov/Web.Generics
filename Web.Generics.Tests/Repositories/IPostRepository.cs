using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inspira.Blog.DomainModel;
using Web.Generics.Infrastructure.DataAccess;
using Web.Generics.ApplicationServices.DataAccess;

namespace Web.Generics.Tests.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
    }
}
