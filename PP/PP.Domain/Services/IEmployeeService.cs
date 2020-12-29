using PP.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PP.Domain.Services
{
    public interface IEmployeeService : IDataService<Angajati>
    {
        Task<Angajati> GetEmployeeByName(string employeeName);

        Task<IEnumerable<Angajati>> GetManagers();

        Task<IEnumerable<Angajati>> GetProgrammers();
    }
}