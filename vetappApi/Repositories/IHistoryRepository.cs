using System.Linq;
using System.Threading.Tasks;
using vetappback.Entities;

namespace vetappApi.Repositories
{
    public interface IHistoryRepository
    {
        IQueryable<History> GetHistoriesAsync(int id);

        Task<History> GetHistoryByIdAsync(int id);

        Task AddHistoryAsync(History model);

        Task UpdateHistoryAsync(History model);

        Task<bool> HistoryExists(int id);

    }
}