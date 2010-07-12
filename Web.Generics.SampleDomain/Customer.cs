using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Generics.SampleDomain
{
    public class Customer
    {
        virtual public Int32 ID { get; set; }
        virtual public String Name { get; set; }
        virtual public IList<Project> Projects { get; set; }
        virtual public IList<User> Users { get; set; }
    }
}
