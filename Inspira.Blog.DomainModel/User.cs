using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspira.Blog.DomainModel
{
    public class User
    {
        virtual public Int32 ID { get; set; }
        virtual public String Name { get; set; }
        virtual public IList<WebLog> Blogs { get; set; }
    }
}
