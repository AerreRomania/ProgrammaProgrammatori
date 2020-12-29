using Microsoft.EntityFrameworkCore;
using PP.Domain.Models;
using PP.Domain.Services;
using PP.EntityFramework.Services.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PP.EntityFramework.Services
{
    public class GenericDataService<T> : IDataService<T> where T : DomainObject
    {
        private readonly PPDbContextFactory _contextFactory;
        private readonly NonQueryDataService<T> nonQueryDataService;

        public GenericDataService(PPDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            nonQueryDataService = new NonQueryDataService<T>(contextFactory);
        }

        public async Task<T> Create(T entity)
        {
            return await nonQueryDataService.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await nonQueryDataService.Delete(id);
        }

        public async Task<T> Get(int id)
        {
            using PPDbContext context = _contextFactory.CreateDbContext();
            T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
            return entity;
            ;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using PPDbContext context = _contextFactory.CreateDbContext();
            IEnumerable<T> entities = await context.Set<T>().ToListAsync();
            return entities;
            ;
        }

        public async Task<T> Update(int id, T entity)
        {
            return await nonQueryDataService.Update(id, entity);
        }
    }
}