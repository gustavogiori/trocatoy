using Infrastructure.Filter;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure
{
    public interface IRepository<T> where T : EntityBase
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(PaginationFilter filter, out int countPages);
        IEnumerable<T> GetByCriteria(Expression<Func<T, bool>> predicate, PaginationFilter filter, out int countPages);
        IEnumerable<T> GetByCriteria(Expression<Func<T, bool>> predicate);
        T GetById(Guid id);
        T Insert(T obj);
        T Update(T obj);
        void Delete(Guid id);
        IQueryable<T> GetTable();
    }
}
