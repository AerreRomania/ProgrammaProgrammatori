using Microsoft.EntityFrameworkCore;
using PP.Domain.Models;
using PP.Domain.Services;
using PP.EntityFramework.Services.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PP.EntityFramework.Services
{
    public class JobTypeDataService : IJobTypeService
    {
        private readonly PPDbContextFactory _contextFactory;
        private readonly NonQueryDataService<JobType> _nonQueryDataService;

        public JobTypeDataService(PPDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<JobType>(contextFactory);
        }

        public async Task<IEnumerable<JobType>> GetAll()
        {
            using (PPDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<JobType> jobTypes = await context.JobType.ToListAsync();
                return jobTypes;

            }
        }

        public async Task<JobType> Get(int id)
        {
            using (PPDbContext context = _contextFactory.CreateDbContext())
            {
                JobType jobType = await context.JobType.FirstOrDefaultAsync(e => e.Id == id);
                return jobType;
            }
        }

        public async Task<JobType> Create(JobType entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<JobType> Update(int id, JobType entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }
    }
}
