using Domaine.Base;
using Domaine.Interfaces;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbFactory _dbFactory;
        private DbSet<TEntity> _dbSet;

        protected DbSet<TEntity> DbSet
        {
            get => _dbSet ??= _dbFactory.DbContext.Set<TEntity>();
        }

        public Repository(DbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public void Add(TEntity entity)
        {
            if (typeof(IAuditEntity).IsAssignableFrom(typeof(TEntity)))
                ((IAuditEntity)entity).CreatedDate = DateTime.Now;
            DbSet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            if (typeof(ISoftDeleteEntity).IsAssignableFrom(typeof(TEntity)))
            {
                ((ISoftDeleteEntity)entity).IsDeleted = true;
                DbSet.Update(entity);
            }
            else
                DbSet.Remove(entity);
        }

        public void Update(TEntity entity)
        {
            if (typeof(IAuditEntity).IsAssignableFrom(typeof(TEntity)))
                ((IAuditEntity)entity).UpdatedDate = DateTime.Now;

            DbSet.Update(entity);
        }

        public void BulkAdd(List<TEntity> entities)
        {
            if (typeof(IAuditEntity).IsAssignableFrom(typeof(TEntity)))
                entities.ForEach(e => ((IAuditEntity)e).CreatedDate = DateTime.Now);

            _dbFactory.DbContext.BulkInsert(entities, new BulkConfig { SetOutputIdentity = true });
        }

        public void BulkDelete(List<TEntity> entities)
        {
            if (typeof(ISoftDeleteEntity).IsAssignableFrom(typeof(TEntity)))
            {
                entities.ForEach(e =>
                {
                    ((ISoftDeleteEntity)e).IsDeleted = true;
                    ((ISoftDeleteEntity)e).DeletedDate = DateTime.Now;
                });

                _dbFactory.DbContext.BulkUpdate(entities);
            }
            else
                _dbFactory.DbContext.BulkDelete(entities);
        }

        public void BulkUpdate(List<TEntity> entities)
        {
            if (typeof(IAuditEntity).IsAssignableFrom(typeof(TEntity)))
                entities.ForEach(e => ((IAuditEntity)e).UpdatedDate = DateTime.Now);

            _dbFactory.DbContext.BulkUpdate(entities);
        }

        public TEntity GetById(object id)
        {
            return DbSet.Find(id);
        }

        public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = DbSet;
            if (include != null)
                query = include(query);
            if (predicate != null)
                query = query.Where(predicate);
            if (typeof(ISoftDeleteEntity).IsAssignableFrom(typeof(TEntity)))
                query = query.Where(x => !((ISoftDeleteEntity)x).IsDeleted);
            if (orderBy != null)
                return orderBy(query).FirstOrDefault();

            return query.FirstOrDefault();
        }

        public IQueryable<TEntity> GetAll()
        {
            if (typeof(ISoftDeleteEntity).IsAssignableFrom(typeof(TEntity)))
                return DbSet.Where(x => !((ISoftDeleteEntity)x).IsDeleted);

            return DbSet;
        }

        public IQueryable<TEntity> GetMuliple(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = DbSet;
            if (include != null)
                query = include(query);
            if (predicate != null)
                query = query.Where(predicate);
            if (typeof(ISoftDeleteEntity).IsAssignableFrom(typeof(TEntity)))
                query = query.Where(x => !((ISoftDeleteEntity)x).IsDeleted);
            if (orderBy != null)
                return orderBy(query);

            return query;
        }
    }
}