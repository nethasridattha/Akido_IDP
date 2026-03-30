using System.Threading.Tasks;

namespace DTPortal.Core.Domain.Repositories
{
    public interface IGenericUnitOfWork
    {
        void Save();

        Task SaveAsync();
    }
}
