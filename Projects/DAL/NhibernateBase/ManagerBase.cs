using System.Collections.Generic;
using DAL.Nhibernate;
using NHibernate;
using NHibernate.Criterion;

namespace DAL.NhibernateBase
{
    public abstract partial class ManagerBase<T, TKey> : IManagerBase<T, TKey>
    {
        #region Declarations

        private readonly INHibernateSession _session;
        protected const int DefaultMaxResults = 1000;

        #endregion

        #region Constructors
        protected ManagerBase()
        {
            _session = NHibernateSessionManager.Instance.Session;
        }
        public ManagerBase(INHibernateSession session)
        {
            _session = session;
            if(_session == null)
                _session = NHibernateSessionManager.Instance.Session;
        }
        #endregion

        #region Get Methods

        public virtual T GetById(TKey id)
        {
            return (T)Session.GetISession().Get(typeof(T), id);
        }
        public IList<T> GetAll()
        {
            return GetByCriteria(DefaultMaxResults);
        }
        public IList<T> GetAll(int maxResults)
        {
            return GetByCriteria(maxResults);
        }
        public IList<T> GetByCriteria(params ICriterion[] criterionList)
        {
            return GetByCriteria(DefaultMaxResults, criterionList);
        }
        public IList<T> GetByCriteria(int maxResults, params ICriterion[] criterionList)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(T)).SetMaxResults(maxResults);

            foreach (ICriterion criterion in criterionList)
                criteria.Add(criterion);

            return criteria.List<T>();
        }
        public IList<T> GetByExample(T exampleObject, params string[] excludePropertyList)
        {
            ICriteria criteria = Session.GetISession().CreateCriteria(typeof(T));
            Example example = Example.Create(exampleObject);

            foreach (string excludeProperty in excludePropertyList)
                example.ExcludeProperty(excludeProperty);

            criteria.Add(example);

            return criteria.List<T>();
        }

        #endregion

        #region CRUD Methods

        public object Save(T entity)
        {
            return Session.GetISession().Save(entity);
        }
        public void SaveOrUpdate(T entity)
        {
            Session.GetISession().SaveOrUpdate(entity);
            Session.GetISession().Flush();
        }
        public void Delete(T entity)
        {
            if (entity.Equals(null))
                return;
            Session.GetISession().Delete(entity);
            Session.GetISession().Flush();
        }
        public virtual void DeleteByPK(int id)
        {
            Session.GetISession().Delete(Session.GetISession().Load<T>(id));
            Session.GetISession().Flush();
        }
        public void Update(T entity)
        {
            Session.GetISession().Update(entity);
        }
        public void Refresh(T entity)
        {
            Session.GetISession().Refresh(entity);
        }

        #endregion

        #region Properties

        /// <summary>
        /// The NHibernate Session object is exposed only to the Manager class.
        /// It is recommended that you...
        /// ...use the the NHibernateSession methods to control Transactions (unless you specifically want nested transactions).
        /// ...do not directly expose the Flush method (to prevent open transactions from locking your DB).
        /// </summary>
        public System.Type Type
        {
            get { return typeof(T); }
        }
        public INHibernateSession Session
        {
            get { return _session; }
        }

        #endregion
    }
}
