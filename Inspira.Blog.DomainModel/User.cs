using System;
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
    }
}
