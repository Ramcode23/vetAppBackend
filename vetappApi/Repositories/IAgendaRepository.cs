using System.Linq;
using System.Threading.Tasks;
using vetappback.Entities;

namespace vetappApi.Repositories
{
    public interface IAgendaRepository
    {
        IQueryable<Agenda> GetAgendasAsync();
        Task<Agenda> GetAgendaByIdAsync(int id);
        Task AddAgendaAsync(Agenda model);
        Task UpdateAgendaAsync(Agenda model);
        Task<bool> AgendaExists(int id);
    }
}