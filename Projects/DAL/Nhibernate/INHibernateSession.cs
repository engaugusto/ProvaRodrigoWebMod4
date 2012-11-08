using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace DAL.Nhibernate
{
    public interface INHibernateSession : IDisposable
    {
        // Methods
        void CommitChanges();
        void Close();
        bool BeginTransaction();
        bool CommitTransaction();
        bool RollbackTransaction();
        ISession GetISession();

        // Properties
        bool HasOpenTransaction { get; }
        bool IsOpen { get; }
    }
}
