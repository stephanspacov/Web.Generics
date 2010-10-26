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
using System.Security.Principal;

namespace Inspira.Blog.DomainModel
{
    public class User
    {
        public User()
        {
            this.ownedBlogs = new List<WebLog>();
            //this.associatedBlogs = new List<WebLog>();
            this.Address = new Address();
        }

        virtual public Int32 ID { get; set; }
        virtual public String Name { get; set; }
		virtual public String Email { get; set; }
        virtual public String Cpf { get; set; }
        virtual public String Photo { get; set; }
        virtual public Int32 NumberOfChildren { get; set; }
        virtual public String Phone { get; set; }
        virtual public Address Address { get; set; }
        virtual public DateTime BirthDate { get; set; }
        virtual public Decimal Salary { get; set; }
        virtual public String Resume { get; set; }
        virtual public String AdditionalInfo { get; set; }
        virtual public Boolean HasAcceptedTerms { get; set; }

        protected virtual IList<WebLog> ownedBlogs { get; set; }
        virtual public void AddBlog(WebLog webLog)
        {
            this.ownedBlogs.Add(webLog);
        }
        virtual public IEnumerable<WebLog> GetOwnedBlogs()
        {
            return this.ownedBlogs;
        }

        //private IList<WebLog> associatedBlogs { get; set; }

        virtual public String Username { get; set; }
        virtual public String Password { get; set; }

        public override string ToString()
        {
            return this.Username;
        }
    }
}
