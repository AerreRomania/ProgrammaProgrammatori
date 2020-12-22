using Microsoft.EntityFrameworkCore;
using PP.Domain.Columns;
using PP.Domain.Models;
using PP.Domain.Services;
using PP.EntityFramework.Services.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PP.EntityFramework.Services
{
    public class ArticleDataService : IArticleService
    {
        private readonly PPDbContextFactory _contextFactory;
        private readonly NonQueryDataService<Articole> nonQueryDataService;

        public ArticleDataService(PPDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            nonQueryDataService = new NonQueryDataService<Articole>(contextFactory);
        }

        public async Task<Articole> Create(Articole entity)
        {
            return await nonQueryDataService.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await nonQueryDataService.Delete(id);
        }

        public async Task<Articole> Get(int id)
        {
            using (PPDbContext context = _contextFactory.CreateDbContext())
            {
                Articole article = await context.Articole.Include(e => e.ProgrammerTask).FirstOrDefaultAsync(i => i.Id == id);
                return article;
            }
        }

        public async Task<IEnumerable<Articole>> GetAll()
        {
            using (PPDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<Articole> article = await context.Articole
                    .Include(p => p.ProgrammerTask)
                    .Include(pa => pa.ProgrammaProgramatoriArticle)
                    .Where(i => i.IdSector == 7).ToListAsync();

                return article;
            }
        }

        public async Task<Articole> GetByName(string articleName)
        {
            using (PPDbContext context = _contextFactory.CreateDbContext())
            {
                Articole article = await context.Articole.Include(e => e.ProgrammerTask).FirstOrDefaultAsync(i => i.Articol == articleName);

                return article;
            }
        }

        public async Task<ArticleGridColumns> GetArticleRow(int articleId)
        {
            using (PPDbContext context = _contextFactory.CreateDbContext())
            {
                var articles = await context.Articole.Where(sId => sId.IdSector == 7).ToListAsync();
                var tasks = await context.ProgrammerTask.Include(p => p.Programmer).ToListAsync();
                var articleDetails = await context.ProgrammaProgramatoriArticle.ToListAsync();

                var row = 
                    (from a in articles
                           where a.Id == articleId
                           select new ArticleGridColumns
                           {
                               PPArticleID = articleDetails.FirstOrDefault(id => id.ArticleID == a.Id).Id,
                               Num = a.Id,
                               Articolo = a.Articol,
                               Model = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.MODEL,
                               Fin = a.Finete,
                               ProgrammerPR = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 1)?.Programmer.Angajat,
                               DataArrivoSchedePr = articleDetails.FirstOrDefault(id => id.ArticleID == a.Id)
                                   ?.DATA_ARRIVO_SCHEDE_PR,
                               StartPr = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 1)?.StartTask,
                               EndPr = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 1)?.EndTask,
                               ProgrammerCa = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 2)?.Programmer.Angajat,
                               StartCa = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 2)?.StartTask,
                               EndCa = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 2)?.EndTask,
                               ConfCamp = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.CONF_CAMP,
                               DataArrivoFilo = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.DATA_ARRIVO_FILO,
                               CapiPrevisti = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.CAPI_PREVISTI,
                               NrMach = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.NR_MACH,
                               DataEntrataInProd = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)
                                   ?.DATA_ENTRATA_IN_PROD,
                               DataArrivoSchema = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.DATA_ARRIVO_SCHEMA,
                               DataInizioSvilTgBase = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)
                                   ?.DATA_INIZIO_SVIL_TG_BASE,
                               DataFineSvilTgBase = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)
                                   ?.DATA_FINE_SVIL_TG_BASE,
                               Gg1 = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.GG1,
                               DataArrivoSchede = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.DATA_ARRIVO_SCHEDE,
                               DataArrivoDisco = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.DATA_ARRIVO_DISCO,
                               ProgrammerPP = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 3)?.Programmer.Angajat,
                               StartPP = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 3)?.StartTask,
                               EndPP = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 3)?.EndTask,
                               Gg2 = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.GG2,
                               Ok = "OK",
                               Gg3 = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.GG3,
                               ProgrammerSv = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 4)?.Programmer.Angajat,
                               StartSv = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 4)?.StartTask,
                               EndSv = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 4)?.EndTask,
                               DataInizioProd = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.DATA_INIZIO_PROD,
                               Gg4 = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.GG4,
                               TotGg = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.TOT_GG,
                               Finish = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.INIZ_FINE,
                               Finished = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.Finished,
                               Weights = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.PESI_X_ITA,
                               Time = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.TEMP,
                               ConfPp = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.CONF_PP,
                               Note = a.Note
                           }).FirstOrDefault();

                return row;
            }
        }

        public async Task<Articole> Update(int id, Articole entity)
        {
            return await nonQueryDataService.Update(id, entity);
        }
    }
}
