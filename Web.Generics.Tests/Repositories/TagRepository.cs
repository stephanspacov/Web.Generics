using Inspira.Blog.DomainModel;
using Web.Generics.ApplicationServices.DataAccess;

namespace Web.Generics.Tests
{
    public class TagRepository : GenericRepository<Tag>
    {
        public TagRepository(IRepositoryContext context) : base(context)
        {
        }
    }

}
