using PP.Domain.Columns;
using PP.Domain.Models;
using System.Threading.Tasks;

namespace PP.Domain.Services
{
    public interface IArticleService : IDataService<Articole>
    {
        Task<ArticleGridColumns> GetArticleRow(int articleId);
    }
}