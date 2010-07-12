using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using Web.Generics.Infrastructure.DataAccess;

namespace Web.Generics.Tests.Repositories
{
    public class UnitOfWork
    {
        private readonly IRepositoryContext context;
        public UnitOfWork(IRepositoryContext context)
        {
            this.context = context;
            this.Posts = new PostRepository(context);
            this.Tags = new TagRepository(context);
        }

        public IPostRepository Posts { get; set; }
        public TagRepository Tags { get; set; }

        public void SaveChanges() {
            context.SaveChanges();
        }
    }
}
