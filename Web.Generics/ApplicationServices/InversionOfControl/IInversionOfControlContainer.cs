using System;
namespace Web.Generics.ApplicationServices.InversionOfControl
{
	public interface IInversionOfControlContainer
	{
		void RegisterType<TFrom, TTo>() where TTo : TFrom;
		void RegisterType(Type interfaceType, Type implementationType);
        void RegisterInstance<T>(T obj);
		T Resolve<T>();
		Object Resolve(Type t);
    }
}
