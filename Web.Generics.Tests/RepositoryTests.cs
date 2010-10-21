using System;
using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Generics.SampleDomain;
using System.Collections.Generic;
using Web.Generics.FluentNHibernate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Web.Generics.Tests
{
	/// <summary>
	/// Summary description for UnitTest1
	/// </summary>
	[TestClass]
	public class RepositoryTests
	{
		IGenericRepository<Project> projectRepository;
        IGenericRepository<Task> taskRepository;
        IGenericRepository<User> userRepository;

		[TestInitialize]
		public void Initialize()
		{
			NHibernateSessionFactoryConfig.ConfigFilePath = @"..\..\..\Web.Generics.Tests\hibernate.cfg.xml";
			//projectRepository = new Web.Generics.GenericNHibernateRepository<Projects>();
			taskRepository = new Web.Generics.GenericNHibernateRepository<Task>();
            userRepository = new GenericNHibernateRepository<User>();
		}


		[TestMethod]
		public void SelectWithOneTableFilter()
		{
			taskRepository.Select(new FilterParameters
			{
				FilterConditions = new[] {
                    new FilterCondition { Property="Project.ID", Value=1 }
                }
			});
		}

		[TestMethod]
		public void SelectWithTwoTableFilter()
		{
			taskRepository.Select(new FilterParameters
			{
				FilterConditions = new[] {
                    new FilterCondition { Property="Project.Customer.ID", Value=1 }
                }
			});
		}

		[TestMethod]
		public void SelectWithLambdaExpressions()
		{
			/*			var project = new Projects { Name = "Projects 1" };
						repository.Insert(project);

						var projectsFromDb = repository.Select(p => p.ID == 1);

						var retrievedOk = (projectsFromDb.Count == 1 && projectsFromDb[0].Name == project.Name);
						Assert.IsTrue(retrievedOk);*/
		}

		[TestMethod]
		public void InsertProjectWithTasks()
		{
			/*			var projectWithTasks = new Projects
						{
							Name = "Projects with tasks",
							Tasks = new [] {
								new Task { Name = "Task 1" },
								new Task { Name = "Task 2" }
							},
						};

						projectWithTasks.Tasks.Select(t => t.Projects = projectWithTasks);

						repository.Insert(projectWithTasks);

						IList<Projects> projects = repository.Select(p => p.ID == projectWithTasks.ID);
						Assert.IsTrue(projects[0].Tasks.Count == 2);*/
		}

        [TestMethod]
        public void x()
        {
            var user = new User { Name = "x" };
            userRepository.Insert(user);

            User userFromDb = userRepository.SelectById(user.ID);
            userFromDb.Customer = new Customer();
            userRepository.Update(userFromDb);

            User userFromDb2 = userRepository.SelectById(user.ID);
            var name = userFromDb2.Customer.Name;     
        }
	}
}
