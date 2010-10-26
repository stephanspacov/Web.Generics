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
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using Web.Generics.ApplicationServices.DataAccess;
using NHibernate.Context;

namespace Web.Generics.Infrastructure.DataAccess.NHibernate
{
    public class NHibernateRepositoryContext : IRepositoryContext
    {
        private ISession session;
		public NHibernateRepositoryContext()
		{
			this.session = ApplicationManager.GetCurrentSession();
		}

		internal NHibernateRepositoryContext(ISession session)
		{
			this.session = session;
		}

		public void SaveChanges()
        {
            session.Flush();
        }

        public IQueryable<T> Query<T>() where T : class
        {
            var query = this.session.Query<T>();
            return (IQueryable<T>)query;
        }

        public void SaveOrUpdate<T>(T obj) where T : class
        {
            this.session.SaveOrUpdate(obj);
        }

        public void Delete(Object obj)
        {
            this.session.Delete(obj);
        }

        public T SelectById<T>(object id)
        {
            return this.session.Get<T>(id);
        }

        public ISession Session { get { return this.session; } set { this.session = value; } }
	}
}