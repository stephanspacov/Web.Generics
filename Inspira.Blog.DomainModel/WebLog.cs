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
            this.Collaborators = new List<User>();
        }

        virtual public Int32 ID { get; set; }
        virtual public String Title { get; set; }
        virtual public DateTime CreatedAt { get; set; }

        virtual public IList<User> Collaborators { get; set; }
        virtual public IList<Post> Posts { get; set; }
		virtual public User Creator { get; set; }

		public override string ToString()
		{
			return this.Title;
		}
    }
}
