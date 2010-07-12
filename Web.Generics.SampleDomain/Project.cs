using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Generics.SampleDomain
{
	public class Project
	{
		virtual public Int32 ID { get; set; }
        virtual public String Name { get; set; }
        virtual public Customer Customer { get; set; }
		virtual public IList<Task> Tasks { get; set; }
	}
}
