using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Web.Generics.DataAnnotations
{
    public enum FieldSize
    {
        small,
        medium,
        large
    }

    public class FormDisplay : System.ComponentModel.DataAnnotations.DisplayColumnAttribute
    {
		public FormDisplay() : base("") { }

        public string GroupName;
        public int Order;
        public FieldSize FieldSize;
    }
}
