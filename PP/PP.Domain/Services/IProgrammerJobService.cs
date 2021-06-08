using PP.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PP.Domain.Services
{
    public interface IProgrammerJobService : IDataService<ProgrammerProgress>
    {
        Task<IEnumerable<ProgrammerProgress>> GetAllByArticle(int IdArticle);
    }
}