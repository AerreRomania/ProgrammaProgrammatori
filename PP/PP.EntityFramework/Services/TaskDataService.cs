using Microsoft.EntityFrameworkCore;
using PP.Domain.Columns;
using PP.Domain.Models;
using PP.Domain.Services;
using PP.EntityFramework.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PP.EntityFramework.Services
{
    public class TaskDataService : ITaskService
    {
        private readonly PPDbContextFactory _contextFactory;
        private readonly NonQueryDataService<ProgrammerTask> _nonQueryDataService;

        public TaskDataService(PPDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<ProgrammerTask>(contextFactory);
        }

        public async Task<ProgrammerTask> Create(ProgrammerTask entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            using PPDbContext context = _contextFactory.CreateDbContext();
            try
            {
                var entity = await context.Set<ProgrammerTask>().FirstOrDefaultAsync((e) => e.ProgrammerTaskID == id);

                context.Set<ProgrammerTask>().Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<IEnumerable<ProgrammerGridColumns>> GetAssigned(int programmerId, bool completed = false)
        {
            using PPDbContext context = _contextFactory.CreateDbContext();
            IEnumerable<ProgrammerTask> tasks = await context.ProgrammerTask.Include(e => e.Programmer).Include(a => a.Article).Where(i => i.ProgrammerID == programmerId && i.TaskCompleted == completed).ToListAsync();
            var articleDetails = await context.ArticleDetails.ToListAsync();

            var orders = await context.Comenzi.Include(c => c.Clienti).ToListAsync();

            var data = (from t in tasks
                select new ProgrammerGridColumns
                {
                    Client = orders.Where(o => o.IdArticol == t.ArticleID).Select(c => c.Clienti.Client).FirstOrDefault(),
                    ArticleHeader = t.Article.Articol,
                    ArticleID = t.ArticleID,
                    EnterInProduction = articleDetails.FirstOrDefault(ad => ad.ArticleID == t.ArticleID)?.DataInizioProd,
                    JobTypeID = t.JobTypeID,
                    Finezza = t.Article.Finete,
                    ProgrammerID = t.ProgrammerID,
                    ProgrammerName = t.Programmer.Angajat,
                    StartDate = t.StartTask,
                    EndDate = t.EndTask,
                    ProgrammerTaskID = t.ProgrammerTaskID
                });

            return data;
        }

        public async Task<IEnumerable<ProgrammerGridColumns>> GetAllAssigned(int programmerId, bool completed = false)
        {
            using PPDbContext context = _contextFactory.CreateDbContext();
            IEnumerable<ProgrammerTask> tasks = await context.ProgrammerTask.Include(e => e.Programmer).Include(a => a.Article).Where(i => i.TaskCompleted == completed && i.ProgrammerID != programmerId).ToListAsync();
            var orders = await context.Comenzi.Include(c => c.Clienti).ToListAsync();
            var articleDetails = await context.ArticleDetails.ToListAsync();

            var data = (from t in tasks
                select new ProgrammerGridColumns
                {
                    Client = orders.Where(o => o.IdArticol == t.ArticleID).Select(c => c.Clienti.Client).FirstOrDefault(),
                    ArticleHeader = t.Article.Articol,
                    ArticleID = t.ArticleID,
                    EnterInProduction = articleDetails.FirstOrDefault(ad => ad.ArticleID == t.ArticleID)?.DataInizioProd,
                    JobTypeID = t.JobTypeID,
                    Finezza = t.Article.Finete,
                    ProgrammerID = t.ProgrammerID,
                    ProgrammerName = t.Programmer.Angajat,
                    StartDate = t.StartTask,
                    EndDate = t.EndTask,
                    ProgrammerTaskID = t.ProgrammerTaskID
                });

            return data;
        }

        public async Task<int> GetTaskId(int programmerId, DateTime start, DateTime end, string articleTitle, int jobTypeId)
        {
            using PPDbContext context = _contextFactory.CreateDbContext();
            var task = await context.ProgrammerTask.Include(pm => pm.Article).FirstOrDefaultAsync(t => 
                t.ProgrammerID == programmerId &&
                t.StartTask == start &&
                t.EndTask == end &&
                t.Article.Articol == articleTitle);
            return task.ProgrammerTaskID;
        }

        public async Task<ProgrammerTask> Get(int id)
        {
            using PPDbContext context = _contextFactory.CreateDbContext();
            ProgrammerTask task = await context.ProgrammerTask.Include(e => e.Programmer).FirstOrDefaultAsync(e => e.ProgrammerTaskID == id);
            return task;
        }

        public async Task<IEnumerable<ProgrammerTask>> GetAll()
        {
            using PPDbContext context = _contextFactory.CreateDbContext();
            try
            {
                IEnumerable<ProgrammerTask> programmerTasks = await context.ProgrammerTask.Include(art => art.Article).Include(p => p.Programmer).ToListAsync();

                return programmerTasks;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<ProgrammerTask> Update(int id, ProgrammerTask entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }
    }
}