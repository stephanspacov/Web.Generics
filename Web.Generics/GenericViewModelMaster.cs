using System;
using System.Collections.Generic;
using System.Web;

namespace Web.Generics
{
    public class GenericViewModelMaster<T> : ViewModelMaster
    {
        public T Instance { get; set; }
        public IList<T> InstanceList { get; set; }
    }
}