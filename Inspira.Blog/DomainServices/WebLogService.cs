using System.Collections.Generic;
using System.Linq;
using Inspira.Blog.DomainModel;
using Web.Generics.ApplicationServices.DataAccess;
using Web.Generics.DomainServices;

namespace Inspira.Blog.DomainServices
{
    public class WebLogService : GenericService<WebLog>
    {
        private readonly IRepository<WebLog> webLogRepository;

        public WebLogService(IRepository<WebLog> webLogRepository) : base(webLogRepository)
        {
            this.webLogRepository = webLogRepository;
        }

        public IList<WebLog> SelectRecent(int qty)
        {
            return this.webLogRepository.Select().OrderByDescending(w => w.CreatedAt).Take(qty).ToList();
        }
    }
}
