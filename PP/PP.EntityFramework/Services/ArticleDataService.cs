using Microsoft.EntityFrameworkCore;
using NLog;
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
            using PPDbContext context = _contextFactory.CreateDbContext();
            Articole article = await context.Articole.Include(e => e.ProgrammerTask).FirstOrDefaultAsync(i => i.Id == id);
            return article;
        }

        public async Task<IEnumerable<Articole>> GetAll()
        {
            using PPDbContext context = _contextFactory.CreateDbContext();
            IEnumerable<Articole> article = await context.Articole
                .Include(p => p.ProgrammerTask)
                .Include(pa => pa.ArticleDetails)
                .Where(i => i.IdSector == 7).ToListAsync();

            return article;
        }

        public async Task<Articole> GetByName(string articleName)
        {
            using PPDbContext context = _contextFactory.CreateDbContext();
            Articole article = await context.Articole.Include(e => e.ProgrammerTask).FirstOrDefaultAsync(i => i.Articol == articleName);

            return article;
        }

        public async Task<ArticleGridColumns> GetArticleRow(int articleId)
        {
            using PPDbContext context = _contextFactory.CreateDbContext();
            var articles = await context.Articole.Where(sId => sId.IdSector == 7).ToListAsync();
            var tasks = await context.ProgrammerTask.Include(p => p.Programmer).ToListAsync();
            var articleDetails = await context.ArticleDetails.ToListAsync();

            var row =
                (from a in articles
                    where a.Id == articleId
                    select new ArticleGridColumns
                    {
                        ArticleDeatilsID = articleDetails.FirstOrDefault(id => id.ArticleID == a.Id)?.Id,
                        Num = a.Id,
                        Articolo = a.Articol,
                        Finezza = a.Finete,
                        MachineNumber = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.MachineNumber,
                        CapiPrevisti = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.CapiPrevisti,
                        DataInizioProd = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.DataInizioProd,
                        Notes = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.Notes,
                        
                        DataArrivoSchedePr = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.DataArrSchedePr,
                        ProgrammerPR = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 1)?.Programmer.Angajat,
                        StartPr = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 1)?.StartTask,
                        EndPr = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 1)?.EndTask,
                        DataConsegnaProt = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.DataConsegnaPr,

                        DataArrSchedaCa = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.DataArrSchedeCa,
                        ProgrammerCa = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 2)?.Programmer.Angajat,
                        StartCa = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 2)?.StartTask,
                        EndCa = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 2)?.EndTask,
                        DataConsegnaCa = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.DataConsegnaCa,

                      DataArrivoTagliaBase = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.DataArrTagliaBase,
                      DataArrivoInzioTagliaBase = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.DataArrInzioTagliaBase,
                      DataArrivoFineTagliaBase = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.DataArrFineTagliaBase,

                      DataArrivoSchedaCo = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.DataArrSchedeCo,
                      ProgrammerCo = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 8)?.Programmer.Angajat,
                      StartCo = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 8)?.StartTask,
                      EndCo = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 8)?.EndTask,
                      DataConsegnaCo = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.DataConsegnaCo,

                      DataArrivoSchedaDisco = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.DataArrSchedaDisco,
                      ProgrammerPP = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 3)?.Programmer.Angajat,
                      DiffGGProdData =  articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.DiffGGProdData,
                      DiffGGProgData =  articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.DiffGGProgData,
                      StartPP = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 3)?.StartTask,
                      EndPP = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 3)?.EndTask,

                      DataConsegnaPP = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.DataConsegnaPP,
                      GG1 = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.GG1,
                      Ok = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.Ok,
                      ProgrammerSvTg = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 4)?.Programmer.Angajat,
                      DataInizioSvilTgBase = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 4)?.StartTask,
                      DataFineSvilTgBase = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 4)?.EndTask,
                        Finish = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.Finish,

                    }).FirstOrDefault();

            return row;
        }

        public async Task<Articole> Update(int id, Articole entity)
        {
            return await nonQueryDataService.Update(id, entity);
        }
    }
}