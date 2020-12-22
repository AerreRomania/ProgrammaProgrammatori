using Microsoft.EntityFrameworkCore;
using PP.Domain.Models;
using PP.Domain.Services.TransactionServices;
using PP.EntityFramework.Services.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PP.EntityFramework.Services
{
    // ReSharper disable once InconsistentNaming
    public class PPArticleDataService : IPPArticleService
    {
        private readonly PPDbContextFactory _contextFactory;
        private readonly NonQueryDataService<ProgrammaProgramatoriArticle> _nonQueryDataService;


        public PPArticleDataService(PPDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<ProgrammaProgramatoriArticle>(contextFactory);
        }

        public async Task<IEnumerable<ProgrammaProgramatoriArticle>> GetAll()
        {
            using (PPDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<ProgrammaProgramatoriArticle> ppArticles = await context.ProgrammaProgramatoriArticle.ToListAsync();
                return ppArticles;

            }
        }

        public async Task<ProgrammaProgramatoriArticle> Get(int id)
        {
            using (PPDbContext context = _contextFactory.CreateDbContext())
            {
                var ppArticle = await context.ProgrammaProgramatoriArticle.FirstOrDefaultAsync(e => e.Id == id);
                return ppArticle;
            }
        }

        public async Task<ProgrammaProgramatoriArticle> Create(ProgrammaProgramatoriArticle entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<ProgrammaProgramatoriArticle> Update(int id, ProgrammaProgramatoriArticle entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }
    }
}
