using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PP.Domain.Models;
using PP.Domain.Services;
using PP.EntityFramework.Services.Common;

namespace PP.EntityFramework.Services
{
    public class ArticleDetailsDataService : IArticleDetailsService
    {
        private readonly PPDbContextFactory _contextFactory;
        private readonly NonQueryDataService<ArticleDetails> _nonQueryDataService;

        public ArticleDetailsDataService(PPDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<ArticleDetails>(contextFactory); ;
        }


        public async Task<IEnumerable<ArticleDetails>> GetAll()
        {
            using PPDbContext context = _contextFactory.CreateDbContext();
            IEnumerable<ArticleDetails> artDetails = await context.ArticleDetails.ToListAsync();
            return artDetails;
        }

        public async Task<ArticleDetails> Get(int id)
        {

            using PPDbContext context = _contextFactory.CreateDbContext();
            var artDetails = await context.ArticleDetails.FirstOrDefaultAsync(e => e.Id == id);
            return artDetails;
        }

        public async Task<ArticleDetails> Create(ArticleDetails entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<ArticleDetails> Update(int id, ArticleDetails entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }
    }
}
