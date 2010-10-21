using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Automapping.Alterations;
using Inspira.Blog.DomainModel;
using FluentNHibernate.Automapping;
using FluentNHibernate;

namespace Inspira.Blog.Infrastructure.DataAccess.FluentNHibernate.Overrides
{
    public class UserOverride : IAutoMappingOverride<User>
    {
        public void Override(AutoMapping<User> mapping)
        {
            mapping.Map(u => u.AdditionalInfo).Nullable();
            mapping.Map(u => u.NumberOfChildren).Nullable();
            mapping.Map(u => u.BirthDate).Nullable();
            mapping.Map(u => u.Cpf).Nullable();
            mapping.Map(u => u.Phone).Nullable();
            mapping.Map(u => u.Photo).Nullable();
            mapping.Map(u => u.Salary).Nullable();
            mapping.Map(u => u.Resume).Nullable();
            mapping.HasMany<WebLog>(Reveal.Member<User>("ownedBlogs")).AsBag().LazyLoad().Cascade.All();
            //mapping.HasManyToMany(u => u.AssociatedBlogs).Table("User_WebLog");
            mapping.Component(u => u.Address);
        }
    }
}
