using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace APIExample
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DbContext Context { get; set; }

        protected RepositoryBase(DbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public T Create(T entity)
        {
            T result = Context.Set<T>().Add(entity).Entity;
            Context.SaveChanges();
            return result;
        }

        public EntityState Delete(T entity)
        {
            var result = Context.Set<T>().Remove(entity).State;
            Context.SaveChanges();
            return result;
        }

        public IQueryable<T> FindAll() =>
            Context.Set<T>().AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            Context.Set<T>().Where(expression).AsNoTracking();

        public EntityState Update(T entity, string propertyName)
        {
            Context.Entry<T>(entity).Property(propertyName).IsModified = false;
            var result = Context.Set<T>().Update(entity).State;
            Context.SaveChanges();
            return result;
        }
    }
}
