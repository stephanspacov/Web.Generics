using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Generics.SampleDomain
{
	// one-to-many relationship with Projects
	public class Task
	{
		virtual public Int32 ID { get; set; }
		virtual public String Name { get; set; }
		virtual public TaskType Type { get; set; }
		virtual public Project Project { get; set; }
        virtual public IList<User> Users { get; set; }
	}
}
