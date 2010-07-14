using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inspira.Blog.WebMvc.Models
{
    public class Pessoa
    {
        public String Nome { get; set; }
        public Int32 Idade { get; set; }
        public Boolean VeioDoFuturo
        {
            get
            {
                if (Idade > 0) return false;
                else return true;
            }
        }
    }
}