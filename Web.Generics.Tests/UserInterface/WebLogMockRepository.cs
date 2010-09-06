using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Generics.ApplicationServices.DataAccess;
using Inspira.Blog.DomainModel;

namespace Web.Generics.Tests.UserInterface
{
    public class WebLogMockRepositoryContext : IRepositoryContext
    {
        List<WebLog> webLogs = new List<WebLog> {
                new WebLog { ID = 1, Title = "Post 1", CreatedAt = new DateTime(2008, 1, 1) },
                new WebLog { ID = 2, Title = "Post 2", CreatedAt = new DateTime(2004, 1, 1) },
                new WebLog { ID = 3, Title = "Post 3", CreatedAt = new DateTime(2010, 1, 1) },
                new WebLog { ID = 4, Title = "Post 4", CreatedAt = new DateTime(2007, 1, 1) },
                new WebLog { ID = 5, Title = "Post 5", CreatedAt = new DateTime(2009, 1, 1) },
                new WebLog { ID = 6, Title = "Post 6", CreatedAt = new DateTime(2005, 1, 1) },
                new WebLog { ID = 7, Title = "Post 7", CreatedAt = new DateTime(2006, 1, 1) },
            };

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void SaveOrUpdate<T>(T obj) where T : class
        {
            throw new NotImplementedException();
        }

        public void Delete(object obj)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Query<T>() where T : class
        {
            return (IQueryable<T>)this.webLogs.AsQueryable();
        }

        public T SelectById<T>(object id)
        {
            throw new NotImplementedException();
        }
    }
}
