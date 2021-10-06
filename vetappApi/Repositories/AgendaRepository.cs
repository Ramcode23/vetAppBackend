using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vetappback.Entities;

namespace vetappApi.Repositories
{
    public class AgendaRepository:IAgendaRepository
    {
        private readonly DataContext dataContext;
        public AgendaRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IQueryable<Agenda> GetAgendasAsync()
        {
            return dataContext.Agendas
                            .Include(a => a.Owner)
                            .ThenInclude(o => o.User)
                            .Include(a => a.Pet)
                            .ThenInclude(p => p.PetType)
                            .Where(a => a.Date >= DateTime.Today.ToUniversalTime())
                            .OrderBy(a => a.Date)
                            .AsQueryable();
        }

        public Task<Agenda> GetAgendaByIdAsync(int id)
        {
            return  dataContext.Agendas
                .Include(a => a.Owner)
                .ThenInclude(o => o.User)
                .Include(a => a.Pet)
                .ThenInclude(p => p.PetType)
                .Where(a => a.Date >= DateTime.Today.ToUniversalTime())
                .OrderBy(a => a.Date)
                .FirstOrDefaultAsync(agenda => agenda.Id == id);
        }

        public Task AddAgendaAsync(Agenda model)
        {
            dataContext.Agendas.AddAsync(model);
            return dataContext.SaveChangesAsync();
        }
        public Task UpdateAgendaAsync(Agenda model)
        {
            dataContext.Entry(model).State = EntityState.Modified;
            return dataContext.SaveChangesAsync();
        }

        public Task<bool> AgendaExists(int id)
        {
            return dataContext.Agendas.AnyAsync(e => e.Id == id);
        }



    }



}