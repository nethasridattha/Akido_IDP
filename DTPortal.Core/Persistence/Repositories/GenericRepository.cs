using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using DTPortal.Core.Domain.Repositories;

namespace DTPortal.Core.Persistence.Repositories
{
    public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        protected readonly TContext Context;
        protected readonly ILogger Logger;
        public GenericRepository(TContext context,
            ILogger logger)
        {
            this.Context = context;
            this.Logger = logger;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            try
            {
                return await Context.Set<TEntity>().FindAsync(id);
            }
            catch(Exception error)
            {
                Logger.LogError(error, "GetByIdAsync:: Database Exception");
                return null;
            }
        }

        public TEntity GetById(int id)
        {
            try
            {
                return Context.Set<TEntity>().Find(id);
            }
            catch (Exception error)
            {
                Logger.LogError(error, "GetById:: Database Exception");
                return null;
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                return await Context.Set<TEntity>().AsNoTracking().ToListAsync();
            }
            catch (Exception error)
            {
                Logger.LogError(error, "GetAllAsync:: Database Exception");
                return null;
            }
        }

        public async Task AddAsync(TEntity model)
        {
            try
            {
                await Context.Set<TEntity>().AddAsync(model);
            }
            catch (Exception error)
            {
                Logger.LogError(error, "AddAsync:: Database Exception");
            }
        }

        public void Add(TEntity model)
        {
            try
            {
                Context.Set<TEntity>().Add(model);
            }
            catch (Exception error)
            {
                Logger.LogError(error, "Add:: Database Exception");
            }
        }

        public void Update(TEntity model)
        {
            try
            {
                Context.Set<TEntity>().Update(model);
                //Context.Attach(model).State = EntityState.Modified;
            }
            catch (Exception error)
            {
                Logger.LogError(error, "Update:: Database Exception");
            }
        }

        public void Reload(TEntity model)
        {
            try
            {
                Context.Entry(model).Reload();
            }
            catch (Exception error)
            {
                Logger.LogError(error, "Update:: Database Exception");
            }
        }
            public void Remove(TEntity model)
        {
            try
            {
                Context.Set<TEntity>().Remove(model);
            }
            catch (Exception error)
            {
                Logger.LogError(error, "Remove:: Database Exception");
            }
        }
    }
}
