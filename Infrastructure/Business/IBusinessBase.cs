using Infrastructure.Filter;
using Infrastructure.Models;
using Infrastructure.Validation;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Business
{
    public interface IBusinessBase<T> where T : EntityBase
    {
        ValidationModel IsValid(T obj);
        void GeraNovoGuid(T obj);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(PaginationFilter filter, out int countPages);
        IEnumerable<T> GetByCriteria(Expression<Func<T, bool>> predicate, PaginationFilter filter, out int countPages);
        IEnumerable<T> GetByCriteria(Expression<Func<T, bool>> predicate);
        T GetById(Guid id);
        Tuple<T, ValidationModel> Insert(T obj);
        Tuple<T, ValidationModel> Update(T obj);
        void Delete(Guid id);
    }
}
