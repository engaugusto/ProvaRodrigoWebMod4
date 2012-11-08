using System;
using DAL.Nhibernate;
using NHibernate;

namespace DAL.NhibernateBase
{
    public class NHibernateSession : INHibernateSession
    {
        #region Declarations
        protected ITransaction transaction = null;
        protected ISession iSession;
        #endregion

        #region Constructor & Destructor
        /// <summary>
        /// NHibernate Session
        /// </summary>
        /// <param name="session"></param>
        public NHibernateSession(ISession session)
        {
            iSession = session;
        }

        /// <summary>
        /// Destrutor
        /// </summary>
        ~NHibernateSession()
        {
            Dispose(true);
        }

        #endregion

        #region IDisposable

        private bool _isDisposed = false;
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(false);
        }
        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="finalizing"></param>
        private void Dispose(bool finalizing)
        {
            if (!_isDisposed)
            {
                // Close Session
                Close();

                // Flag as disposed.
                _isDisposed = true;
                if (!finalizing)
                    GC.SuppressFinalize(this);
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Comitar
        /// </summary>
        public void CommitChanges()
        {
            if (HasOpenTransaction)
                CommitTransaction();
            else
                iSession.Flush();
        }
        
        /// <summary>
        /// Fechar
        /// </summary>
        public void Close()
        {
            if (iSession.IsOpen)
            {
                try
                {
                    iSession.Flush();
                }
                catch (Exception ex) { }
                
                iSession.Close();
            }
        }

        /// <summary>
        /// Começa Transação
        /// </summary>
        /// <returns></returns>
        public bool BeginTransaction()
        {
            bool result = !HasOpenTransaction;
            if (result)
                transaction = iSession.BeginTransaction();
            return result;
        }
        
        /// <summary>
        /// Comitar Transação
        /// </summary>
        /// <returns></returns>
        public bool CommitTransaction()
        {
            bool result = HasOpenTransaction;
            if (result)
            {
                try
                {
                    transaction.Commit();
                    transaction = null;
                }
                catch (HibernateException)
                {
                    transaction.Rollback();
                    transaction = null;
                    throw;
                }
            }
            return result;
        }
        
        /// <summary>
        /// Voltar Transação
        /// </summary>
        /// <returns></returns>
        public bool RollbackTransaction()
        {
            bool result = HasOpenTransaction;
            if (result)
            {
                transaction.Rollback();
                transaction.Dispose();
                transaction = null;

                // I dont know why, but it seems that after you rollback a transaction you need to reset the session.
                // Personally, I dislike this; I find it inefficent, and it means that I have to expose a method to
                // get an ISession from the NHibernateSessionManager...does anyone know how to get around this problem?
                iSession.Close();
                iSession.Dispose();
                iSession = NHibernateSessionManager.Instance.CreateISession();
            }
            return result;
        }

        /// <summary>
        /// Get ISession
        /// </summary>
        /// <returns></returns>
        public ISession GetISession()
        {
            return iSession;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Transação esta aberta
        /// </summary>
        public bool HasOpenTransaction
        {
            get
            {
                return (transaction != null && !transaction.WasCommitted && !transaction.WasRolledBack);
            }
        }

        /// <summary>
        /// Esta Aberto
        /// </summary>
        public bool IsOpen
        {
            get { return iSession.IsOpen; }
        }
        #endregion
    }
}
