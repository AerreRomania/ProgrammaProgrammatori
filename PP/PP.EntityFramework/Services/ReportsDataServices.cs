using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PP.Domain.Columns;
using PP.Domain.Models;
using PP.Domain.Services;
using PP.EntityFramework.Services.Common;

namespace PP.EntityFramework.Services
{
    public class ReportsDataServices:IReportsService
    {
        private readonly PPDbContextFactory _contextFactory;
        private readonly NonQueryDataService<AnalysisArticle> nonQueryDataService;
        private readonly IProgrammerJobService _programmerJobService;
        public ReportsDataServices(PPDbContextFactory contextFactory, IProgrammerJobService programmerJobService)
        {
            _contextFactory = contextFactory;
            nonQueryDataService = new NonQueryDataService<AnalysisArticle>(contextFactory);
            _programmerJobService = programmerJobService;
        }
        
        public async Task<IEnumerable<AnalysisArticle>> GetAnalysisArticleAsync(int article, DateTime start, DateTime end, int client, string stag)
        {
            using PPDbContext context = _contextFactory.CreateDbContext();
            var articles = await context.Articole.Where(sId => sId.IdSector == 7 && sId.Id==article).ToListAsync();
            var tasks = await context.ProgrammerTask.Where(a=> a.ArticleID==article).ToListAsync();
            var orders = await context.Comenzi.Include(c => c.Clienti).Where(a=>a.IdClient==client).ToListAsync();
            var programmerProgresses = await _programmerJobService.GetAll();
            List<ProgrammerProgress> progresses = new List<ProgrammerProgress>();
            foreach (var programmerProgress in programmerProgresses)
            {
                programmerProgress.ArticleTitle = articles.FirstOrDefault(id => id.Id == programmerProgress.Progress.ArticleID)?.Articol;
                progresses.Add(programmerProgress);
            }
            var progress = progresses.Where(a => a.ArticleTitle == articles[0].Articol && a.WorkLocationID==1 && a.WorkLocationID==2 && a.StartWork >= start && a.EndWork <= end).ToList();
            var data = (from t in tasks
                        from p in progress
                        where t.ArticleTitle == p.ArticleTitle
                        select new ProgrammerGridColumns
                        {
                            Client = orders.Where(o => o.IdArticol == article).Select(c => c.Clienti.Client).FirstOrDefault(),
                            ArticleHeader = articles[0].Articol,
                            ArticleID = t.ArticleID,
                            JobTypeID = t.JobTypeID,
                            Finezza = t.Article.Finete,
                            ProgrammerID = p.WorkLocationID,
                            ProgrammerName = t.Programmer.Angajat,
                            StartDate = p.StartWork,
                            EndDate = (DateTime)p.EndWork ,
                            ProgrammerTaskID = t.ProgrammerTaskID,
                            Note = t.Note

                        }); 
            List<AnalysisArticle> final = new List<AnalysisArticle>();
           foreach(var d in data)
            {
                var f = new AnalysisArticle() 
                { 
                    Client=d.Client,
                    Stagione=stag,
                    Finezza=d.Finezza,
                    Programmer=d.ProgrammerName,
                    JobTypeID=d.JobTypeID,
                    ComputerHours=(d.EndDate-d.StartDate).TotalHours,
                    RetilineaHours= (d.EndDate - d.StartDate).TotalHours,
                    


                };
                final.Add(f);
            }
            return final;
        }
        public Task<IEnumerable<AnalysisArticle>> GetAll()
        {
            return null;
        }

       public Task<AnalysisArticle> Get(int id)
        {
            return null;
        }

       public Task<AnalysisArticle> Create(AnalysisArticle entity)
        {
            return null;
        }

        public Task<AnalysisArticle> Update(int id, AnalysisArticle entity)
        {
            return null;
        }

        public Task<bool> Delete(int id)
        {
            return null;
        }

        
    }
}
