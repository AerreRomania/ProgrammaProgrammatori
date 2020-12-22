using Microsoft.EntityFrameworkCore;
using PP.Domain.Models;
using PP.Domain.Services;
using PP.EntityFramework.Services.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PP.EntityFramework.Services
{
    public class ProgrammerDataService : IEmployeeService
    {

        private readonly PPDbContextFactory _contextFactory;
        private readonly NonQueryDataService<Angajati> _nonQueryDataService;

        public ProgrammerDataService(PPDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<Angajati>(contextFactory);
        }

        public async Task<Angajati> Create(Angajati entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }

        public async Task<Angajati> Get(int id)
        {
            using (PPDbContext context = _contextFactory.CreateDbContext())
            {
                Angajati entity = await context.Angajati.Include(a => a.ProgrammerTask).FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            };
        }

        public async Task<IEnumerable<Angajati>> GetAll()
        {
            using (PPDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<Angajati> entities = await context.Angajati.Where(e => e.Mansione.Contains("MANAGER")).ToListAsync();
                return entities;
            };
        }

        public async Task<Angajati> GetEmployeeByName(string employeeName)
        {
            using (PPDbContext context = _contextFactory.CreateDbContext())
            {
                Angajati entity = await context.Angajati.Include(a => a.ProgrammerTask).FirstOrDefaultAsync((e) => e.Angajat == employeeName);
                return entity;
            };
        }

        public async Task<IEnumerable<Angajati>> GetManagers()
        {
            using (PPDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<Angajati> entities = await context.Angajati.Where(e => e.Mansione.Contains("MANAGER")).ToListAsync();
                return entities;
            };
        }

        public async  Task<IEnumerable<Angajati>> GetProgrammers()
        {
            using (PPDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<Angajati> entities = await context.Angajati.Where(e => e.Mansione.Contains("PROGRAMMER")).ToListAsync();
                return entities;
            };
        }

        public async Task<Angajati> Update(int id, Angajati entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }


    }
}
