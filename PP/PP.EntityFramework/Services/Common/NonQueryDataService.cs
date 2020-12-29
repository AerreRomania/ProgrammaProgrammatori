using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PP.Domain.Models;
using System;
using System.Threading.Tasks;

namespace PP.EntityFramework.Services.Common
{
    public class NonQueryDataService<T> where T : DomainObject
    {
        private readonly PPDbContextFactory _contextFactory;

        public NonQueryDataService(PPDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<T> Create(T entity)
        {
            using PPDbContext context = _contextFactory.CreateDbContext();
            try
            {
                EntityEntry<T> createdEntity = await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();
                return createdEntity.Entity;
            }
            catch (Exception e)
            {
                throw e;
            }

            ;
        }

        public async Task<bool> Delete(int id)
        {
            using PPDbContext context = _contextFactory.CreateDbContext();
            T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
            return true;
            ;
        }

        public async Task<T> Update(int id, T entity)
        {
            using PPDbContext context = _contextFactory.CreateDbContext();
            entity.Id = id;
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
            return entity;
            ;
        }
    }
}