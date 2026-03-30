using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using DTPortal.Core.Domain.Repositories;

namespace DTPortal.Core.Persistence.Repositories
{
    public class GenericUnitOfWork<TContext> : IGenericUnitOfWork
        where TContext : DbContext
    {
        protected readonly TContext Context;

        public GenericUnitOfWork(TContext context)
        {
            this.Context = context;
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
