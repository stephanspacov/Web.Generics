using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inspira.Blog.DomainModel;
using Web.Generics.DomainServices;
using Web.Generics.Infrastructure.DataAccess;
using Inspira.Blog.Infrastructure.DataAccess;

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
