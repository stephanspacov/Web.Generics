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
using Newtonsoft.Json.Serialization;
using System.Reflection;
using NHibernate.Proxy;
using Newtonsoft.Json;

namespace Web.Generics
{
	public class NHibernateContractResolver : DefaultContractResolver
	{
		private static readonly MemberInfo[] NHibernateProxyInterfaceMembers = typeof(INHibernateProxy).GetMembers();

		protected override List<MemberInfo> GetSerializableMembers(Type objectType)
		{
			var members = base.GetSerializableMembers(objectType);

			members.RemoveAll(memberInfo =>
							  (IsMemberPartOfNHibernateProxyInterface(memberInfo)) ||
							  (IsMemberDynamicProxyMixin(memberInfo)) ||
							  (IsMemberMarkedWithIgnoreAttribute(memberInfo, objectType)) ||
							  (IsMemberInheritedFromProxySuperclass(memberInfo, objectType)));

			var actualMemberInfos = new List<MemberInfo>();

			foreach (var memberInfo in members)
			{
				var infos = memberInfo.DeclaringType.BaseType.GetMember(memberInfo.Name);
				actualMemberInfos.Add(infos.Length == 0 ? memberInfo : infos[0]);
			}

			return actualMemberInfos;
		}

		private static bool IsMemberDynamicProxyMixin(MemberInfo memberInfo)
		{
			return memberInfo.Name == "__interceptors";
		}

		private static bool IsMemberInheritedFromProxySuperclass(MemberInfo memberInfo, Type objectType)
		{
			return memberInfo.DeclaringType.Assembly == typeof(INHibernateProxy).Assembly;
		}

		private static bool IsMemberMarkedWithIgnoreAttribute(MemberInfo memberInfo, Type objectType)
		{
			var infos = typeof(INHibernateProxy).IsAssignableFrom(objectType)
						  ? objectType.BaseType.GetMember(memberInfo.Name)
						  : objectType.GetMember(memberInfo.Name);

			return infos[0].GetCustomAttributes(typeof(JsonIgnoreAttribute), true).Length > 0;
		}

		private static bool IsMemberPartOfNHibernateProxyInterface(MemberInfo memberInfo)
		{
			return Array.Exists(NHibernateProxyInterfaceMembers, mi => memberInfo.Name == mi.Name);
		}
	}
}
