using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetByCriteria(Func<T, bool> predicate);
        T GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
    }
}
