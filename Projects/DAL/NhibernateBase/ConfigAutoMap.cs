using System;
using FluentNHibernate.Automapping;

namespace DAL.NhibernateBase
{
    public class ConfigAutoMap : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type.Namespace == "DAL.DbModel";
        }
    }
}
