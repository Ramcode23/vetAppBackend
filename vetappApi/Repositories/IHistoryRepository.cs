using System.Linq;
using System.Threading.Tasks;
using vetappback.Entities;

namespace vetappApi.Repositories
{
    public interface IHistoryRepository
    {
<<<<<<< HEAD
        IQueryable<History> GetHistoriesAsync(int id);
=======
        IQueryable<History> GetHistoriesAsync();
>>>>>>> origin/main

        Task<History> GetHistoryByIdAsync(int id);

        Task AddHistoryAsync(History model);

        Task UpdateHistoryAsync(History model);

        Task<bool> HistoryExists(int id);

    }
}