﻿using System;
using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Generics.Infrastructure.DataAccess;
using Web.Generics.Infrastructure.DataAccess.NHibernate;
using Web.Generics.Tests.Repositories;
using Web.Generics.Infrastructure.DataAccess.EntityFramework;
using Inspira.Blog.DomainModel;

namespace Web.Generics.Tests
{
	/// <summary>
	/// Summary description for UnitTest1
	/// </summary>
	[TestClass]
	public class Infrastructure_Repository_Tests
	{
        static GenericRepository<WebLog> webLogRepository;
        static TagRepository tagRepository;

        /*
1 x 1 -> usuário x perfil
1 x n -> post x comentário && blog x post
0 x n -> post x categoria
n x n -> post x tag
auto-relacionamento -> categoria x sub-categoria
enum -> status publicação (post e comentário)
nullable columns -> 
relacionamento circular (a -> b -> a)
relacionamento trinario
         */

		[TestInitialize]
		public void TestInitialize()
		{
            NHibernateSessionFactoryConfig.ConfigFilePath = @"..\..\..\Web.Generics.Tests\hibernate.cfg.xml";
            NHibernateSessionFactoryConfig.RepositoryType = typeof(PostRepository);
            //var nhibernateSession = FluentNHibernate.FluentNHibernateHelper<Post>.OpenSession();

            //var context = new NHibernateRepositoryContext(nhibernateSession);

            var context = new EntityFrameworkRepositoryContext(new BlogContext());
            webLogRepository = new GenericRepository<WebLog>(context);
            tagRepository = new TagRepository(context);
            
            // zero o banco antes de executar cada teste
            tagRepository.Select().ToList().ForEach(t => tagRepository.Delete(t));
            webLogRepository.Select().ToList().ForEach(w => webLogRepository.Delete(w));
            context.SaveChanges();
		}

    	[TestMethod]
		public void SelectWithLambdaExpressions()
		{
            var title = Guid.NewGuid().ToString();
            var webLog = new WebLog { Owner = new User { Name = "user " + title }, Title="weblog " + title, CreatedAt = DateTime.Now };
            var post = new Post { Title = title, Text = "oi", CreatedAt = DateTime.Now, LastUpdatedAt = DateTime.Now, PublishedAt = DateTime.Now };

            webLog.Posts.Add(post);
            post.WebLog = webLog;

            webLogRepository.SaveOrUpdate(webLog);
            webLogRepository.SaveChanges();

            var posts = (from w in webLogRepository.Select()
                            from p in w.Posts
                            where p.Title == title
                            select p).ToList(); 
            Assert.AreEqual(1, posts.Count);
		}

		[TestMethod]
		public void InsertPostWithNewTagsAndComments()
		{
            var title = Guid.NewGuid().ToString();

            var webLog = new WebLog { Owner = new User { Name = "user " + title }, Title = "weblog " + title, CreatedAt = DateTime.Now };            
            var post = new Post
            {
                Title = title,
                Text = "oi",
                CreatedAt = DateTime.Now,
                LastUpdatedAt = DateTime.Now,
                PublishedAt = DateTime.Now,
                Comments = new [] {
                    new Comment { ApprovedAt = DateTime.Now, Text="comment1", Title="oie", AuthorEmail="oxe", AuthorName="name", AuthorUrl="url", CreatedAt=DateTime.Now },
                    new Comment { ApprovedAt = DateTime.Now, Text="comment2", Title="oie", AuthorEmail="oxe", AuthorName="name", AuthorUrl="url", CreatedAt=DateTime.Now }
                },
                Tags = new [] {
                    new Tag { CreatedAt = DateTime.Now, Text = "tag1" },
                    new Tag { CreatedAt = DateTime.Now, Text = "tag2" },
                }
            };

            webLog.Posts.Add(post);
            post.WebLog = webLog;
            foreach (var c in post.Comments) c.Post = post;

            webLogRepository.SaveOrUpdate(webLog);
            webLogRepository.SaveChanges();

            var posts = (from w in webLogRepository.Select()
                         from p in w.Posts
                         where p.Title == title
                         select p).ToList(); 

            Assert.AreEqual(1, posts.Count);
		}

        [TestMethod]
        public void InsertPostWithExistingTagsAndComments()
        {
            var title = Guid.NewGuid().ToString();

            var tags = tagRepository.Select(t => t.Text.Contains("tag1")).ToList();

            var webLog = new WebLog { Owner = new User { Name = "user " + title }, Title = "weblog " + title, CreatedAt = DateTime.Now };
            var post = new Post
            {
                Title = title,
                Text = "Post com tags existentes",
                CreatedAt = DateTime.Now,
                LastUpdatedAt = DateTime.Now,
                PublishedAt = DateTime.Now,
                Tags = tags
            };

            webLog.Posts.Add(post);
            post.WebLog = webLog;

            webLogRepository.SaveOrUpdate(webLog);
            webLogRepository.SaveChanges();

            var posts = (from w in webLogRepository.Select()
                         from p in w.Posts
                         where p.Title == title
                         select p).ToList();

            Assert.AreEqual(1, posts.Count);
        }

        [TestMethod]
        public void InsertPostWithNullableProperty()
        {
            var title = Guid.NewGuid().ToString();

            var webLog = new WebLog { Owner = new User { Name = "user " + title }, Title = "weblog " + title, CreatedAt = DateTime.Now };
            var post = new Post
            {
                Title = title,
                Text = "Post sem data de publicação",
                CreatedAt = DateTime.Now,
                LastUpdatedAt = DateTime.Now
            };

            webLog.Posts.Add(post);
            post.WebLog = webLog;

            webLogRepository.SaveOrUpdate(webLog);

/*            var posts = (from w in webLogRepository.Select()
                         from p in w.Posts
                         where p.Title == title
                         select p).ToList(); */
            var posts = webLogRepository.Select().SelectMany(w => w.Posts).Where(p => p.Title == title).ToList();

            Assert.AreEqual(1, posts.Count);
        }
    }
}