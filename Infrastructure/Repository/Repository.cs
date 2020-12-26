using Infrastructure.Filter;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Infrastructure
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        protected DbSet<T> table = null;
        DbContext _context;
        public Repository(DbContext _context)
        {
            table = _context.Set<T>();
            this._context = _context;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return table.ToList();
        }
        public virtual IEnumerable<T> GetAll(PaginationFilter filter, out int countPages)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = GetTable()
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToList();

            countPages = GetTable().Count();

            return pagedData;
        }
        public virtual IQueryable<T> GetTable()
        {
            return table;
        }
        public virtual T GetById(Guid id)
        {
            return GetTable().FirstOrDefault(x => x.Id == id);
        }
        public virtual T Insert(T obj)
        {
            if (GConvert.IsGuidEmpty(obj.Id))
                obj.Id = Guid.NewGuid();

            table.Add(obj);

            return obj;

        }
        public virtual T Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;

            return obj;
        }
        public virtual void Delete(Guid id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }
        public virtual int GetLastId()
        {
            var item = table.LastOrDefault();
            return GConvert.ToInt32(item.GetType().GetProperty("Id", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance));
        }
        public virtual void Save()
        {
            _context.SaveChanges();
        }
        public virtual IEnumerable<T> GetByCriteria(Expression<Func<T, bool>> predicate)
        {
            return table.Where(predicate);
        }
        public virtual IEnumerable<T> GetByCriteria(Expression<Func<T, bool>> predicate, PaginationFilter filter, out int countPages)
        {
            var tableResult = GetTable().Where(predicate);
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);

            var pagedData = tableResult
               .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
               .Take(validFilter.PageSize)
               .ToList();

            countPages = tableResult.Count();

            return pagedData;
        }
    }
}
