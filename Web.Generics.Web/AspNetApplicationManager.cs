using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Web;
using NHibernate.Context;
using NHibernate;

namespace Web.Generics
{
    public static partial class AspNetApplicationManager
    {
        public static void BindSessionToCurrentContext()
        {
            Trace.WriteLine(DateTime.Now + "    Opening a new session factory (" + HttpContext.Current.Request.RawUrl + ")", "NHTests");
            var session = ApplicationManager.SessionFactory.OpenSession();
            ManagedWebSessionContext.Bind(
                HttpContext.Current,
                session
             );
        }

        public static void UnbindSession()
        {
			ISession session = ManagedWebSessionContext.Unbind(HttpContext.Current, ApplicationManager.SessionFactory);
            if (session != null)
            {
                if (session.Transaction != null && session.Transaction.IsActive)
                {
                    session.Transaction.Rollback();
                }
                // else session.Flush();

                Trace.WriteLine(DateTime.Now + "    Closing session(" + HttpContext.Current.Request.RawUrl + ")", "NHTests");
                session.Close();
            }
        }
    }
}
