using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PP.Domain.Columns;
using PP.Domain.Models;

namespace PP.Domain.Services
{
    public interface ITaskService : IDataService<ProgrammerTask>
    {
           Task<IEnumerable<ProgrammerGridColumns>> GetAssigned(int programmerId, bool completed = false);
           Task<IEnumerable<ProgrammerGridColumns>> GetAllAssigned(int programmerId, bool completed = false);
           Task<int> GetTaskId(int programmerId, DateTime start, DateTime end, string articleTitle, int jobTypeId);
    }
}
