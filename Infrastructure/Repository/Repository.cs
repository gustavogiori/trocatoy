using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbSet<T> table = null;
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
        public virtual T GetById(object id)
        {
            return table.Find(id);
        }
        public virtual void Insert(T obj)
        {
            table.Add(obj);
        }
        public virtual void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public virtual void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }
        public virtual void Save()
        {
            _context.SaveChanges();
        }

        public virtual IEnumerable<T> GetByCretiria(Func<T, bool> predicate)
        {
            return table.Where(predicate);
        }
    }
}
