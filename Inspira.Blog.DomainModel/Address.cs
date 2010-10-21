using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inspira.Blog.DomainModel
{
    public class Address
    {
        public String StreetName { get; set; }
        public String Number { get; set; }
        public String Complement { get; set; }
        public String ZipCode { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        //public String Country { get; set; }
    }
}
