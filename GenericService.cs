using System;
using System.Collections.Generic;
using log4net.Repository.Hierarchy;

namespace Web.Generics
{
	public class GenericService<T>
	{
        private readonly IGenericRepository<T> genericRepository;
        private readonly ILogger logger;

        public GenericService(IGenericRepository<T> genericRepository)
            : this(genericRepository, null)
        {
        }

        public GenericService(IGenericRepository<T> genericRepository, ILogger logger)
        {
            this.genericRepository = genericRepository;
            this.logger = logger;
        }

		virtual public Int32 Insert(T obj)
		{
            WriteToLog("Insert");
			return this.genericRepository.Insert(obj);
		}

		virtual public Int32 Update(T obj)
		{
            WriteToLog("Update");
			return this.genericRepository.Update(obj);
		}

		virtual public Int32 Delete(T obj)
		{
            WriteToLog("Delete");
			return this.genericRepository.Delete(obj);
		}

		virtual public IList<T> Select()
		{
            WriteToLog("Select");
			return this.genericRepository.Select();
		}

        virtual public IList<T> Select(FilterParameters filter)
        {
            WriteToLog("Select");
            return this.genericRepository.Select(filter) ;
        }

        virtual public Int32 Count()
        {
            WriteToLog("Select");
            return this.genericRepository.Count();
        }

        virtual public Int32 Count(FilterParameters filter)
        {
            WriteToLog("Count");
            return this.genericRepository.Count(filter);
        }

		virtual public T SelectById(Object id)
		{
            WriteToLog("SelectById (" + id + ")");
			return this.genericRepository.SelectById(id);
		}

        private void WriteToLog(string method)
        {
            if (logger != null)
            {
                var remoteIP = System.Web.HttpContext.Current.Request.ServerVariables["remote_addr"];
                var loggedUserName = System.Web.HttpContext.Current.User.Identity.Name;
                logger.Info("{0} {1}: {2} {3}", typeof(T).Name, method, remoteIP, loggedUserName);
            }
        }

        public System.Collections.IList SelectByType(Type relatedEntityType)
        {
            return this.genericRepository.SelectByType(relatedEntityType);
        }
    }
}
