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

namespace Inspira.Blog.DomainModel
{
    public class Post
    {
        public Post()
        {
			this.WebLog = new WebLog();
            this.Tags = new List<Tag>();
            this.Comments = new List<Comment>();
        }

        virtual public Int32 ID { get; set; }
        virtual public String Title { get; set; }
        virtual public String Text { get; set; }
        virtual public DateTime CreatedAt { get; set; }
        virtual public Boolean IsPublished { get; set; }
        virtual public DateTime? PublishedAt { get; set; }
        virtual public DateTime LastUpdatedAt { get; set; }

        virtual public WebLog WebLog { get; set; }
        virtual public IList<Tag> Tags { get; set; }
        virtual public IList<Comment> Comments { get; set; }
    }
}
