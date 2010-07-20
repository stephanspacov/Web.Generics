using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspira.Blog.DomainModel
{
    public class WebLog
    {
        public WebLog()
        {
            this.Posts = new List<Post>();
            this.Owners = new List<User>();
        }

        virtual public Int32 ID { get; set; }
        virtual public String Title { get; set; }
        virtual public DateTime CreatedAt { get; set; }

        virtual public IList<User> Owners { get; set; }
        virtual public IList<Post> Posts { get; set; }

		public override string ToString()
		{
			return this.Title;
		}
    }
}
