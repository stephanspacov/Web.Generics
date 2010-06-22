using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Web.Generics.ApplicationServices;

namespace Web.Generics.Infrastructure.InversionOfControl.Unity
{
	public class UnityInversionOfControlContainer : IInversionOfControlContainer
	{
		UnityContainer container;
		public UnityInversionOfControlContainer()
		{
			container = new UnityContainer();
		}

		public void RegisterGenericType<TFrom, TTo>() where TTo : TFrom
		{
			container.RegisterType<TFrom, TTo>();
		}

		public void RegisterType<TFrom, TTo>() where TTo : TFrom
		{
			container.RegisterType<TFrom, TTo>();
		}

		public void RegisterType(Type fromType, Type toType)
		{
			container.RegisterType(fromType, toType);
		}

		T IInversionOfControlContainer.Resolve<T>()
		{
			try
			{
				return container.Resolve<T>();
			} catch (Exception exc) {
				throw new UnboundInterfaceException("Could not resolve type: " + typeof(T), exc);
			}
		}

		public object Resolve(Type t)
		{
			try
			{
				return container.Resolve(t);
			}
			catch (Exception exc)
			{
				throw new UnboundInterfaceException("Could not resolve type: " + t, exc);
			}
		}
	}
}
