using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Generics.Tests.Repositories;
using Inspira.Blog.DomainModel;
using Web.Generics.Infrastructure.DataAccess;

namespace Web.Generics.Tests.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(IRepositoryContext context) : base(context)
        {
        }
    }
}
