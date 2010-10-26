/*
Copyright 2010 Inspira Tecnologia.
All Rights Reserved.

Contact: Thiago Alves <thiago.alves@inspira.com.br>

This file is part of Web.Generics

Web.Generics is free software: you can redistribute it and/or modify
it under the terms of the GNU Lesser General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Web.Generics is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public License
along with Web.Generics.  If not, see <http://www.gnu.org/licenses/>.
*/

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Web.Generics.ApplicationServices.InversionOfControl;

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

		public T Resolve<T>()
		{
			try
			{
				return container.Resolve<T>();
			}
            catch (Exception exc)
            {
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

        public void RegisterInstance<T>(T instance)
        {
            container.RegisterInstance<T>(instance);
        }
    }
}
