using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Inspira.Blog.Infrastructure.Repositories;
using Inspira.Blog.DomainModel;
using Inspira.Blog.Infrastructure.Repositories.Implementation;
using NHibernate.Cfg;
using FluentNHibernate.Automapping;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Cfg;
using System.Data.Objects;

namespace Inspira.Blog.WebMvc.Tests
{
    [TestClass]
    public class PostTests
    {
        UnitOfWork unitOfWork { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            // entity framework 4
            //this.unitOfWork = new UnitOfWork(new EntityFrameworkRepositoryContext(new BlogContext()));
            
            // nhibernate
            var configuration = GetNHibernateConfig();
            this.unitOfWork = new UnitOfWork(new NHibernateRepositoryContext(configuration.BuildSessionFactory().OpenSession()));
        }

        private static Configuration GetNHibernateConfig()
        {

            var configuration = new Configuration();
            configuration.Configure(@"..\..\..\Tests\hibernate.cfg.xml");
            configuration.AddAssembly(typeof(Post).Assembly);
            var autoMapping = AutoMap.AssemblyOf<Post>()
                            .Alterations(x => x.AddFromAssembly(typeof(PostRepository).Assembly))
                            .Setup(s =>
                                s.FindIdentity =
                                    property => property.Name == "ID")
                            .Where(t => t.BaseType != typeof(Exception) && t.BaseType != typeof(ObjectContext))
                            .Conventions.Add(
                                PrimaryKey.Name.Is(pk => "ID"),
                                DefaultCascade.SaveUpdate(),
                                DefaultLazy.Never(),
                                ForeignKey.EndsWith("_ID"),
                                ConventionBuilder.Reference.Always(x => x.Not.Nullable()),
                                ConventionBuilder.Reference.Always(x => x.Cascade.None()),
                                ConventionBuilder.HasMany.Always(x => x.LazyLoad()),
                                ConventionBuilder.HasMany.Always(x => x.Inverse()),
                                ConventionBuilder.HasManyToMany.Always(x => x.Table(x.TableName.Replace("ListTo", "").Substring(0, x.TableName.Length - 10)))
                            );

            autoMapping.GetType().GetMethod("UseOverridesFromAssemblyOf").MakeGenericMethod(typeof(PostRepository)).Invoke(autoMapping, null);

            var sessionFactory = Fluently.Configure(configuration).Mappings(m => m.AutoMappings.Add(autoMapping)).BuildSessionFactory();
            return configuration;
        }

        [TestMethod]
        public void NotApprovedPostsCannotBeShown()
        {
            foreach (var post in this.unitOfWork.Posts.Select().ToList())
            {
                this.unitOfWork.Posts.Delete(post);
            }

            this.unitOfWork.SaveChanges();

            var title = "Test " + new Random().Next();

            this.unitOfWork.Posts.SaveOrUpdate(new Post { IsPublished = true, Title = title, Text = "oi", CreatedAt = DateTime.Now, LastUpdatedAt = DateTime.Now, PublishedAt = DateTime.Now });
            this.unitOfWork.Posts.SaveOrUpdate(new Post { IsPublished = false, Title = title, Text = "text", CreatedAt = DateTime.Now, LastUpdatedAt = DateTime.Now, PublishedAt = DateTime.Now });

            this.unitOfWork.SaveChanges();

            IList<Post> posts = this.unitOfWork.Posts.Select(p=>p.IsPublished == true).ToList();
            Assert.AreEqual(1, posts.Count);
        }

        [TestMethod]
        public void UpdateDataWithUnitOfWork()
        {
            var post1 = new Post { IsPublished = true, Title = "oi", Text = "oi", CreatedAt = DateTime.Now, LastUpdatedAt = DateTime.Now, PublishedAt = DateTime.Now };
            var post2 = new Post { IsPublished = false, Title = "falta publicar", Text = "text", CreatedAt = DateTime.Now, LastUpdatedAt = DateTime.Now, PublishedAt = DateTime.Now };

            this.unitOfWork.Posts.SaveOrUpdate(post1);
            this.unitOfWork.Posts.SaveOrUpdate(post2);
            this.unitOfWork.SaveChanges();

            post1 = unitOfWork.Posts.Select(p => p.ID == post1.ID).FirstOrDefault();
            post2 = unitOfWork.Posts.Select(p => p.ID == post2.ID).FirstOrDefault();

            unitOfWork.SaveChanges();

            post1.Title = "Meu post";
            post2.Title = "Meu post 2";

            unitOfWork.SaveChanges();

            var p1 = unitOfWork.Posts.Select(p => p.ID == post1.ID).FirstOrDefault();
            var p2 = unitOfWork.Posts.Select(p => p.ID == post2.ID).FirstOrDefault();

            Assert.AreEqual(p1.Title, post1.Title);
            Assert.AreEqual(p2.Title, post2.Title);
        }
    }
}
