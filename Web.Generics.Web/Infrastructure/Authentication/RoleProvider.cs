/*
Copyright 2010 Inspira Tecnologia.
All Rights Reserved.

Contact: Thiago Alves <thiago.alves@inspira.com.br>

This file is part of Web.Generics

Web.Generics is free software: you can redistribute it and/or modify
it under the terms of the GNU Lesser General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Web.Generics is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public License
along with Web.Generics.  If not, see <http://www.gnu.org/licenses/>.
*/

﻿using System;
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

        public RoleProvider()
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

            Assembly thisAssembly = Assembly.GetAssembly(typeof(RoleProvider));
            string currentAssembly = thisAssembly.FullName;

            try
            {
                if (assembly != currentAssembly)
                    thisAssembly = Assembly.Load(assembly);
            }
            catch (Exception e)
            {
                throw new ApplicationException("Inspira RoleProvider: Error loading the repository's assembly.", e);
            }

            try
            {
                Type repType = thisAssembly.GetType(repositoryType);
                repository = (IMembershipRoleRepository)Activator.CreateInstance(repType, true);

            }
            catch (Exception e)
            {
                throw new ApplicationException("Inspira RoleProvider: Error creating an instance of the repository.", e);
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            VerifyParameters(usernames, "username");
            VerifyParameters(roleNames, "role");

            foreach (string user in usernames)
                repository.AddUsersToRoles(new string[] { user }, roleNames);
        }

        public override string ApplicationName
        {
            get
            {
                System.Configuration.Configuration configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                System.Web.Configuration.RoleManagerSection roleSection = (System.Web.Configuration.RoleManagerSection)configuration.GetSection("system.web/roleManager");

                foreach (System.Configuration.ProviderSettings p in roleSection.Providers)
                {
                    if (p.Name == roleSection.DefaultProvider)
                        return p.Parameters["applicationName"];
                }

                return null;
            }
            set { }
        }

        public override void CreateRole(string roleName)
        {
            if (repository.RoleExists(roleName))
                throw new ApplicationException("Inspira RoleProvider: Role already exists.");

            repository.CreateRole(roleName);
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            VerifyParameters(new string[] { roleName }, "role");

            if (throwOnPopulatedRole)
            {
                string[] users = repository.GetUsersInRole(roleName);
                if (users.Length > 0)
                    throw new ApplicationException("Inspira RoleProvider: roleName has one or more members and throwOnPopulatedRole is true.");
            }
            if (repository.RoleExists(roleName))
            {
                repository.DeleteRole(roleName, false);
                return true;
            }
            return false;
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            VerifyParameters(new string[] { roleName }, "role");
            VerifyParameters(new string[] { usernameToMatch }, "user");

            return repository.FindUsersInRole(roleName, usernameToMatch);
        }

        public override string[] GetAllRoles()
        {
            return repository.GetAllRoles();
        }

        public override string[] GetRolesForUser(string username)
        {
            VerifyParameters(new string[] { username }, "username");

            return repository.GetRolesForUser(username);
        }

        public override string[] GetUsersInRole(string roleName)
        {
            VerifyParameters(new string[] { roleName }, "role");

            if (!repository.RoleExists(roleName))
                throw new ApplicationException(String.Format("Inspira RoleProvider: The role '{0}' does not exists.", roleName));

            return repository.GetUsersInRole(roleName);
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            VerifyParameters(new string[] { username }, "user");

            return repository.IsUserInRole(username, roleName);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            VerifyParameters(usernames, "username");
            VerifyParameters(roleNames, "role");

            //foreach(string user in usernames)
            //    if (System.Web.Security.Membership.GetUser(user) == null)
            //        throw new ProviderException(String.Format("Inspira RoleProvider: The user '{0}' does not exist.", user));

            //foreach (string role in roleNames)
            //    if (!repository.RoleExists(role))
            //        throw new ProviderException(String.Format("Inspira RoleProvider: The role '{0}' does not exists.", role));

            foreach (string user in usernames)
                repository.RemoveUsersFromRoles(new string[] { user }, roleNames);

        }

        public override bool RoleExists(string roleName)
        {
            return repository.RoleExists(roleName);
        }

        private void VerifyParameters(string[] parameters, string singularParameterName)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i] == null)
                    throw new ArgumentNullException(String.Format("Inspira RoleProvider: The '{0}' of index '{1}' cannot be null", singularParameterName, i));
                else if (parameters[i] == String.Empty)
                    throw new ArgumentException(String.Format("Inspira RoleProvider: The '{0}' if index '{1}' cannot be empty", singularParameterName, i));
            }
        }
    }
}
