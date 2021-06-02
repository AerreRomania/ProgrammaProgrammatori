using PP.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PP.Domain.Services
{
    public interface IReportsService: IDataService<AnalysisArticle>
    {
        Task<IEnumerable<AnalysisArticle>> GetAnalysisArticleAsync(int articlename, DateTime start, DateTime end, int client, string stagione);
      
    }
}
