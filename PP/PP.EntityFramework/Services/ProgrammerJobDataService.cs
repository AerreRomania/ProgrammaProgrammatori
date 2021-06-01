using Microsoft.EntityFrameworkCore;
using PP.Domain.Models;
using PP.Domain.Services;
using PP.EntityFramework.Services.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PP.EntityFramework.Services
{
    public class ProgrammerJobDataService : IProgrammerJobService
    {
        private readonly PPDbContextFactory _contextFactory;
        private readonly NonQueryDataService<ProgrammerProgress> _nonQueryDataService;

        public ProgrammerJobDataService(PPDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<ProgrammerProgress>(contextFactory);
        }

        public async Task<IEnumerable<ProgrammerProgress>> GetAll()
        {
            using PPDbContext context = _contextFactory.CreateDbContext();
            var progress = await context.ProgrammerProgress.Include(p => p.Progress).ToListAsync();
            return progress;
        }

        public async Task<ProgrammerProgress> Get(int id)
        {
            using PPDbContext context = _contextFactory.CreateDbContext();
            var progress = await context.ProgrammerProgress.FirstOrDefaultAsync(a => a.ProgrammerProgressID == id);
            return progress;
        }

        public async Task<ProgrammerProgress> Create(ProgrammerProgress entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<ProgrammerProgress> Update(int id, ProgrammerProgress entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }
       
        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }
    }
}