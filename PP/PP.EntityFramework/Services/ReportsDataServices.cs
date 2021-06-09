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
    public class ReportsDataServices : IReportsService
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
        public async Task<IEnumerable<Stagiuni>> GetStagiuniAsync()
        {
            using PPDbContext context = _contextFactory.CreateDbContext();
            var stagiunis = await context.Stagiuni.ToListAsync();
            return stagiunis;
        }
        public async Task<IEnumerable<Clienti>> GetClientisAsync()
        {
            using PPDbContext context = _contextFactory.CreateDbContext();
            var clients = await context.Clienti.ToListAsync();
            return clients;
        }
        public async Task<IEnumerable<Angajati>> GetAngajatiAsync()
        {
            using PPDbContext context = _contextFactory.CreateDbContext();
            var angajatis = await context.Angajati.Where(a => a.Mansione == "PROGRAMMER").ToListAsync();
            return angajatis;
        }

        public async Task<IEnumerable<AnalisiOperatore>> GetAnalisiOperatore(int operatorId, int year, int Month, int client)
        {
            using PPDbContext context = _contextFactory.CreateDbContext();
            var result = (from a in context.ProgrammerTask
                          from b in context.ProgrammerProgress
                          where a.ProgrammerTaskID == b.ProgrammerTaskID
                          from c in context.Articole
                          where a.ArticleID == c.Id
                          from d in context.Comenzi
                          where a.ArticleID == d.IdArticol
                          from e in context.Clienti
                          where d.IdClient == e.Id
                          from f in context.Angajati
                          where a.ProgrammerID == f.Id
                          from g in context.JobType
                          where a.JobTypeID == g.JobTypeID 
                          where a.ProgrammerID == operatorId && b.StartWork.Month == Month
                          && b.StartWork.Year == year && e.Id == client
                          select new AnalisiOperatore 
                          {
                              JobTypeName = g.JobTypeName,
                              Articol=c.Articol
                          }
                    ).Distinct().ToList();
            return result;
        }
        public async Task<IEnumerable<AnalysisArticle>> GetAnalysisArticleAsync(int article, DateTime start, DateTime end, int client)
        {
            List<int> JobTypes = new List<int>();
            List<string> locations = new List<string>();
            List<AnalysisArticle> listreport = new List<AnalysisArticle>();
            using PPDbContext context = _contextFactory.CreateDbContext();
            var final = (from a in context.ProgrammerTask
                         from b in context.ProgrammerProgress
                         where a.ProgrammerTaskID == b.ProgrammerTaskID
                         from c in context.Articole
                         where a.ArticleID == c.Id
                         from d in context.Comenzi
                         where a.ArticleID == d.IdArticol
                         from e in context.Clienti
                         where d.IdClient == e.Id
                         from f in context.Angajati
                         where a.ProgrammerID == f.Id
                         from g in context.WorkLocation
                         where b.WorkLocationID == g.WorkLocationID
                         where a.ArticleID == article && b.EndWork != null && b.StartWork >= start && b.EndWork <= end && e.Id == client
                         select new AnalysisArticle
                         {
                             Programmer = f.Angajat,
                             JobTypeID = a.JobTypeID,
                             WorkLocationName = g.Location,
                             dbHours = Math.Round((b.EndWork - b.StartWork).Value.TotalHours, 2),
                         }
                       ).Distinct().ToList();

            foreach (var f in final)
            {
                if (JobTypes.Contains(f.JobTypeID)) continue;
                else JobTypes.Add(f.JobTypeID);
            }
            foreach (var f in final)
            {
                if (locations.Contains(f.WorkLocationName)) continue;
                else locations.Add(f.WorkLocationName);
            }
            foreach (var jobs in JobTypes)
            {
                var row = new AnalysisArticle();
                for (int i = 0; i < final.Count; i++)
                {
                    var curr = final[i];
                    row.Programmer = curr.Programmer;
                    row.JobTypeID = jobs;
                    if (curr.JobTypeID == jobs)
                    {

                        if (curr.WorkLocationName == "Computer") row.ComputerHours = curr.dbHours;
                        else if (curr.WorkLocationName == "ComputerMacchina") row.ComputerMachineHours = curr.dbHours;
                        else if (curr.WorkLocationName == "Macchina") row.MachineHours = curr.dbHours;

                    }

                }
                row.Total = Math.Round(row.MachineHours + row.ComputerHours + row.ComputerMachineHours, 2);
                listreport.Add(row);
            }
            return listreport;
        }
        public async  Task<IEnumerable<AnalisiOperatore>> GetAnalisiOperatore()
            {
                return null;
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
