using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inspira.Blog.DomainModel;
using Web.Generics.Infrastructure.DataAccess;

namespace Inspira.Blog.Infrastructure.DataAccess
{
    public class UnitOfWork
    {
        private readonly IRepositoryContext context;
        public UnitOfWork(IRepositoryContext context)
        {
            this.context = context;
            this.Users = new GenericRepository<User>(context);
            this.Blogs = new GenericRepository<WebLog>(context);
        }

        public IRepository<User> Users { get; private set; }
        public IRepository<WebLog> Blogs { get; private set; }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
