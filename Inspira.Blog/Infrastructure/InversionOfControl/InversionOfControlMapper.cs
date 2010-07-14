using Inspira.Blog.DomainModel;
using Web.Generics.ApplicationServices.DataAccess;
using Web.Generics.ApplicationServices.InversionOfControl;

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
