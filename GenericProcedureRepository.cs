using System;
using System.Collections.Generic;

namespace Web.Generics
{
	public class GenericProcedureRepository<T> : IGenericRepository<T> {
		public Int32 Insert(T obj) { return 5; }
		public Int32 Update(T obj) { return 6; }
		public Int32 Delete(T obj) { return 7; }
        public IList<T> Select() { throw new Exception("Puxando de procs"); }
        public T SelectById(Object id) { return default(T); }
        public Int32 Count() { return 0; }
        public Int32 Count(FilterParameters parameters) { return 0; }
        public IList<T> Select(FilterParameters parameters) { return null; }
        public System.Collections.IList SelectByType(Type relatedEntityType) { return null; }


        public IList<T> Select(HtmlHelpers.IWebGrid filter)
        {
            throw new NotImplementedException();
        }

        public int Count(HtmlHelpers.IWebGrid filter)
        {
            throw new NotImplementedException();
        }
    }
}
