using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Nhibernate
{
    public interface IManagerBase<T, TKey>
    {
        // Get Methods
        T GetById(TKey Id);
        IList<T> GetAll();
        IList<T> GetAll(int maxResults);

        /*
        IList<T> GetByCriteria(params ICriterion[] criterionList);
        IList<T> GetByCriteria(int maxResults, params ICriterion[] criterionList);
        IList<T> GetByExample(T exampleObject, params string[] excludePropertyList);
        */

        // CRUD Methods
        object Save(T entity);
        void SaveOrUpdate(T entity);
        void Delete(T entity);
        void DeleteByPK(int id);
        void Update(T entity);
        void Refresh(T entity);

        // Properties
        System.Type Type { get; }
        INHibernateSession Session { get; }
    }
}
