using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PP.Domain.Columns;
using PP.Domain.Models;
using PP.Domain.Services;
using PP.WPF.ViewModels;

namespace PP.WPF.Commands
{
    public class FinishedCommand : AsyncCommandBase
    {
        private HomeViewModel _viewModel;
        private IArticleService _articleService;
        private IArticleDetailsService _articleDetailsService;
        private ITaskService _taskService;


        public FinishedCommand(HomeViewModel viewModel, IArticleService articleService, IArticleDetailsService articleDetailsService, ITaskService taskService)
        {
            _viewModel = viewModel;
            _articleService = articleService;
            _taskService = taskService;
            _articleDetailsService = articleDetailsService;
        }

        public override async Task ExecuteAsync(object parameter)
        {

            IEnumerable<Articole> articles = await _articleService.GetAll();
            IEnumerable<ProgrammerTask> programerTasks = await _taskService.GetAll();
            IEnumerable<ArticleDetails> ppArticles = await _articleDetailsService.GetAll();

            var tasks = programerTasks as ProgrammerTask[] ?? programerTasks.ToArray();
            var articleDetails = ppArticles as ArticleDetails[] ?? ppArticles.ToArray();
            var articoles = articles as Articole[] ?? articles.ToArray();

            var mergedData =
                from a in articoles
                join meta in articleDetails on a.Id equals meta.ArticleID
                select new ArticleGridColumns
                {
                    ArticleDeatilsID = articleDetails.FirstOrDefault(id => id.ArticleID == a.Id)?.Id,
                    Num = a.Id,
                    Articolo = a.Articol,
                    Finezza = a.Finete,
                    MachineNumber = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.MachineNumber,
                    CapiPrevisti = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.CapiPrevisti,
                    DataInizioProd = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.DataInizioProd,
                    Notes = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.Notes,

                    DataArrivoSchedePr = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.DataArrSchedePr,
                    ProgrammerPR = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 1)?.Programmer.Angajat,
                    StartPr = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 1)?.StartTask,
                    EndPr = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 1)?.EndTask,
                    DataConsegnaProt = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.DataConsegnaPr,

                    DataArrSchedaCa = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.DataArrSchedeCa,
                    ProgrammerCa = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 2)?.Programmer.Angajat,
                    StartCa = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 2)?.StartTask,
                    EndCa = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 2)?.EndTask,
                    DataConsegnaCa = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.DataConsegnaCa,

                    DataArrivoTagliaBase = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.DataArrTagliaBase,
                    DataArrivoInzioTagliaBase = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.DataArrInzioTagliaBase,
                    DataArrivoFineTagliaBase = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.DataArrFineTagliaBase,

                    DataArrivoSchedaCo = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.DataArrSchedeCo,
                    ProgrammerCo = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 8)?.Programmer.Angajat,
                    StartCo = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 8)?.StartTask,
                    EndCo = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 8)?.EndTask,
                    DataConsegnaCo = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.DataConsegnaCo,

                    DataArrivoSchedaDisco = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.DataArrSchedaDisco,
                    ProgrammerPP = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 3)?.Programmer.Angajat,
                    DiffGGProdData = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.DiffGGProdData,
                    DiffGGProgData = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.DiffGGProgData,
                    StartPP = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 3)?.StartTask,
                    EndPP = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 3)?.EndTask,

                    DataConsegnaPP = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.DataConsegnaPP,
                    GG1 = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.GG1,
                    Ok = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.Ok,
                    ProgrammerSvTg = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 4)?.Programmer.Angajat,
                    DataInizioSvilTgBase = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 4)?.StartTask,
                    DataFineSvilTgBase = tasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 4)?.EndTask,
                    Finish = articleDetails.FirstOrDefault(i => i.ArticleID == a.Id)?.Finish,
                };

            _viewModel.PpArticles.Clear();

            foreach (var row in mergedData)
            {
                if (row.Finish == true)
                {
                    _viewModel.PpArticles.Add(row);
                }
            }
        }
    }
}
