using System;
namespace Web.Generics.ApplicationServices
{
	public interface IInversionOfControlContainer
	{
		void RegisterType<TFrom, TTo>() where TTo : TFrom;
		void RegisterType(Type interfaceType, Type implementationType);
		T Resolve<T>();
		Object Resolve(Type t);
	}
}
