using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Web.Generics.ApplicationServices.Authentication;
using Web.Generics.Infrastructure.Logging;

namespace Web.Generics.Infrastructure.Authentication
{
    public class RoleProvider : System.Web.Security.RoleProvider
    {
        private IMembershipRoleRepository repository;
		private static ILogger logger = Log4NetLogger.GetLogger<RoleProvider>();

        public RoleProvider()
        {
			try
			{
				string assembly = String.Empty;
				string repositoryType = String.Empty;

				System.Configuration.Configuration configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
				System.Web.Configuration.RoleManagerSection roleSection = (System.Web.Configuration.RoleManagerSection)configuration.GetSection("system.web/roleManager");

				foreach (System.Configuration.ProviderSettings p in roleSection.Providers)
				{
					if (p.Name == roleSection.DefaultProvider)
					{
						assembly = p.Parameters["repositoryAssembly"];
						repositoryType = p.Parameters["repository"];
					}
				}

				Assembly thisAssembly = Assembly.GetAssembly(typeof(MembershipProvider));
				string currentAssembly = thisAssembly.FullName;

				if (assembly != currentAssembly)
					thisAssembly = Assembly.Load(assembly);

				Type repType = thisAssembly.GetType(repositoryType);
				repository = (IMembershipRoleRepository)Activator.CreateInstance(repType, true);
			}
			catch (Exception exc)
			{
				logger.Error("Não foi possível construir o RoleProvider: " + exc.ToString());
				throw new Exception("Não foi possível construir o RoleProvider: " + exc.ToString(), exc);
			}
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
