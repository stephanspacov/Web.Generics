using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Web.Generics.Infrastructure.InversionOfControl;
using Web.Generics.ApplicationServices.InversionOfControl;
using Inspira.Blog.DomainModel;
using Web.Generics.Infrastructure.DataAccess;

namespace Inspira.Blog.Infrastructure.InversionOfControl
{
    public class InversionOfControlMapper : IInversionOfControlMapper
    {
        public void DefineMappings(IInversionOfControlContainer container)
        {
            container.RegisterType<IRepository<User>, GenericRepository<User>>();
            container.RegisterType<IRepository<WebLog>, GenericRepository<WebLog>>();
            container.RegisterType<IRepository<Post>, GenericRepository<Post>>();
        }
    }
}
