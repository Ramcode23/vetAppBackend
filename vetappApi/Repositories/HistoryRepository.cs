using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vetappback.Entities;

namespace vetappApi.Repositories
{
<<<<<<< HEAD
    public class HistoryRepository:IHistoryRepository
=======
    public class HistoryRepository
>>>>>>> origin/main
    {
         private readonly DataContext dataContext;

        public HistoryRepository(DataContext dataContext)
        {
              this.dataContext = dataContext;
        }
       


       
<<<<<<< HEAD
            public IQueryable<History> GetHistoriesAsync(int id)
        {
             return dataContext.Histories.Where(x=>x.PetId==id).AsQueryable();
=======
            public IQueryable<History> GetHistoriesAsync()
        {
             return dataContext.Histories.AsQueryable();
>>>>>>> origin/main
          
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