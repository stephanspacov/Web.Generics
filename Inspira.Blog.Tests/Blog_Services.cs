using System;
using System.Linq;
using Inspira.Blog.DomainModel;
using Inspira.Blog.Infrastructure.DataAccess.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Generics.ApplicationServices.DataAccess;
using Web.Generics.Infrastructure.DataAccess.EntityFramework;
using Web.Generics.UserInterface.HtmlHelpers;

namespace Inspira.Blog.Tests
{
    [TestClass]
    public class Blog_Services
    {
        GenericRepository<WebLog> WebLogs;
        String webLogID = Guid.NewGuid().ToString();
        String postID = Guid.NewGuid().ToString();

        [TestInitialize]
        public void Initialize()
        {
            var context = new EntityFrameworkRepositoryContext(new BlogContext());
            WebLogs = new GenericRepository<WebLog>(context);

            var title = Guid.NewGuid().ToString();

            var user = new User { Name = "user " + title };
            var webLog = new WebLog { Title = webLogID, CreatedAt = DateTime.Now };
            var post = new Post { Title = postID, Text = "oi", CreatedAt = DateTime.Now, LastUpdatedAt = DateTime.Now, PublishedAt = DateTime.Now };

            webLog.Collaborators.Add(user);
            user.Blogs.Add(webLog);
            webLog.Posts.Add(post);
            post.WebLog = webLog;

            WebLogs.SaveOrUpdate(webLog);
        }

        [TestMethod]
        public void List_Posts_By_Date_Range()
        {
            //WebLogs.Where(w => w.ID == 12).SingleOrDefault().Posts.Where();

            Assert.Inconclusive();
        }

        [TestMethod]
        public void Add_Comment_In_Post()
        {
            var comment = new Comment();
            WebLogs.SingleOrDefault(w => w.ID == 12).Posts.SingleOrDefault(p => p.ID == 144).Comments.Add(comment);

            var item = new WebLog();
            item.Title = "jhji";
            //WebLogs.Add(item);

            WebLogs.SaveChanges();

            Assert.Inconclusive();
        }
    }
}
