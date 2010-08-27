using System.Web;
using Web.Generics.Web.Infrastructure.DataAccess.NHibernate;
using System;
using NHibernate;
using NHibernate.Context;
using FluentNHibernate.Automapping;
using NHibernate.Cfg;
using FluentNHibernate.Conventions.Helpers;

public class NHibernateSessionPerRequestModule : IHttpModule
{

    private static readonly ISessionFactory _sessionFactory;



    static NHibernateSessionPerRequestModule()
    {

        _sessionFactory = CreateSessionFactory();

    }



    public void Init(HttpApplication context)
    {

        context.BeginRequest += BeginRequest;

        context.EndRequest += EndRequest;

    }



    public static ISession GetCurrentSession()
    {

        return _sessionFactory.GetCurrentSession();

    }



    public void Dispose() { }



    private static void BeginRequest(object sender, EventArgs e)
    {

        ISession session = _sessionFactory.OpenSession();

        session.BeginTransaction();

        CurrentSessionContext.Bind(session);

    }



    private static void EndRequest(object sender, EventArgs e)
    {

        ISession session = CurrentSessionContext.Unbind(_sessionFactory);



        if (session == null) return;



        try
        {

            session.Transaction.Commit();

        }

        catch (Exception)
        {
            session.Transaction.Rollback();
        }
        finally
        {
            session.Close();
            session.Dispose();
        }
    }

    private static ISessionFactory CreateSessionFactory()
    {
        var configuration = new Configuration();
        configuration.Configure();
        configuration.AddAssembly(typeof(Advogado).Assembly);

        var autoMapping = AutoMap.AssemblyOf<Advogado>()
                 .Alterations(x => x.AddFromAssembly(typeof(AdvogadoMappingOverride).Assembly))
                 .Setup(s =>
                     s.FindIdentity =
                         property => property.Name == "ID")
                 .Where(t => t.BaseType != typeof(Exception))
                 .Conventions.Add(
                     PrimaryKey.Name.Is(pk => "ID"),
                     DefaultCascade.All(),
                     DefaultLazy.Always(),
                     ForeignKey.EndsWith("_ID")//,
                 );

        return configuration.BuildSessionFactory();
    }
}

