using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Generics.Infrastructure.DataAccess;
using Inspira.Blog.DomainModel;

namespace Web.Generics.Tests
{
    public class TagRepository : GenericRepository<Tag>
    {
        public TagRepository(IRepositoryContext context) : base(context)
        {
        }
    }

}
