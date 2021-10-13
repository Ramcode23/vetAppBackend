using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vetappback.Entities;

namespace vetappApi.Repositories
{
    public class HistoryRepository:IHistoryRepository
    {
         private readonly DataContext dataContext;

        public HistoryRepository(DataContext dataContext)
        {
              this.dataContext = dataContext;
        }
       


       
            public IQueryable<History> GetHistoriesAsync(int id)
        {
             return dataContext.Histories.Where(x=>x.PetId==id).AsQueryable();
          
        }


        public Task<History> GetHistoryByIdAsync(int id)
        {
            return dataContext.Histories.FirstOrDefaultAsync();
        }

        public Task AddHistoryAsync(History model)
        {
            dataContext.Histories.AddAsync(model);
            return dataContext.SaveChangesAsync();
        }
        public Task UpdateHistoryAsync(History model)
        {
            dataContext.Entry(model).State = EntityState.Modified;
            return dataContext.SaveChangesAsync();
        }

        public Task<bool> HistoryExists(int id)
        {
            return dataContext.Histories.AnyAsync(e => e.Id == id);
        }

     

    }
    }