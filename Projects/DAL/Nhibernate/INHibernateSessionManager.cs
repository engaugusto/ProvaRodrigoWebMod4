using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace DAL.Nhibernate
{
    public interface INHibernateSessionManager : IDisposable
    {
        // Methods
        INHibernateSession CreateSession();
        ISession CreateISession();
        // Properties
        INHibernateSession Session { get; }
    }
}
