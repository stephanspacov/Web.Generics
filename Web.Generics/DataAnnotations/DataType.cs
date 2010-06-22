using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Web.Generics.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class FileUploadAttribute: DataTypeAttribute
    {
        public FileUploadAttribute()
            : base(DataType.Text)
        {

        }
    }
}
