using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Web.Generics.SampleDomain
{
    public class UsuarioCadastro
    {
        virtual public Int32 ID { get; set; }
        virtual public String Login { get; set; }
        virtual public Perfil Perfil { get; set; }
        virtual public DateTime LastActivityDate { get; set; }
    }
}
