using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Automapping;
using System.Reflection;
using System.Web;
using Web.Generics.ApplicationServices.InversionOfControl;

namespace Web.Generics
{
    public class ApplicationConfiguration
    {
        public ApplicationConfiguration()
        {
            this.Fluent = new FluentConfiguration();
            this.InversionOfControl = new InversionOfControlConfiguration();
        }

        public FluentConfiguration Fluent { get; set; }
        public InversionOfControlConfiguration InversionOfControl { get; set; }
        public Assembly DomainAssembly { get; set; }

        public class FluentConfiguration
        {
            public IAutomappingConfiguration MappingConfigurationInstance { get; set; }
			public Assembly OverrideAssembly { get; set; }
		}

        public class InversionOfControlConfiguration
        {
			public InversionOfControlConfiguration()
			{				
			}
            public IInversionOfControlMapper MapperInstance { get; set; }
        }
    }
}
