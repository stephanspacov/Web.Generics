using Inspira.Blog.DomainModel;
using Web.Generics.ApplicationServices.DataAccess;

namespace Web.Generics.Tests.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(IRepositoryContext context) : base(context)
        {
        }
    }
}
