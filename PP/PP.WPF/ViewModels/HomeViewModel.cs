using PP.Domain.Columns;
using PP.Domain.Models;
using PP.Domain.Services;
using PP.Domain.Services.TransactionServices;
using PP.WPF.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace PP.WPF.ViewModels
{
    public class TaskStatus
    {
        public int Id { get; set; }
        public Brush StatusBrush { get; set; }
        public string Caption { get; set; }
    }

    public class HomeViewModel : ViewModelBase
    {
        private readonly IArticleService _articleService;
        private readonly IEmployeeService _employeeService;
        private readonly ITaskService _taskService;
        private readonly IPPArticleService _ppArticleService;

        public ObservableCollection<JobType> Labels { get; set; }
        public ObservableCollection<TaskStatus> Statuses { get; set; }

        public static string[] JobsTypes =
        {
            "None",
            "Prototipo",
            "Campionario",
            "Preproduzione",
            "Svillupo Taglie",
            "Schede Tehnice",
            "Riparazione/Ass.Rep.",
            "Prove tecniche",
            "Vacanza"
        };

        public static Color[] JobsColorTypes =
        {
            Color.FromRgb(225, 225, 220),
            Color.FromRgb(50, 205, 50),
            Color.FromRgb(255, 255, 51),
            Color.FromRgb(211, 160, 221),
            Color.FromRgb(0, 191, 255),
            Color.FromRgb(135, 206, 250),
            Color.FromRgb(255, 51, 51),
            Color.FromRgb(205,92,92),
            Color.FromRgb(169,169,169),

        };

        public static string[] States = { "In Progress", "Finished" };
        public static Brush[] BrushStates = { new LinearGradientBrush(Colors.Green, Colors.Yellow, 45.0), new SolidColorBrush(Colors.Red) };



        public HomeViewModel(IArticleService articleService,
                             IEmployeeService employeeService,
                             ITaskService taskService,
                             IPPArticleService ppArticleService)
        {
            _articleService = articleService;
            _employeeService = employeeService;
            _taskService = taskService;
            _ppArticleService = ppArticleService;

            SaveTaskCommand = new SaveTaskCommand(this, taskService);
            ChangeTaskCommand = new ChangeTaskCommand(this, _taskService);
            DeleteTaskCommand = new DeleteTaskCommand(this, _taskService);
            Labels = CreateLabels();
            Statuses = CreateStates();
            GetEmployees();
            GetTasks();
            GetArticles();
        }

        private ArticleGridColumns _selectedArticleRow;

        public ArticleGridColumns SelectedArticleRow
        {
            get => _selectedArticleRow;
            set
            {
                _selectedArticleRow = value;

                var addedRow = AddArticleDetails(_selectedArticleRow);
                var row = _ppArticles.FirstOrDefault(i => i.Num == _selectedArticleRow.Num);
                _ppArticles.Remove(row);
                _ppArticles.Add(addedRow);
                _ppArticles = new ObservableCollection<ArticleGridColumns>(_ppArticles.OrderBy(n => n.Num));

                OnPropertyChanged(nameof(SelectedArticleRow));
                OnPropertyChanged(nameof(PpArticles));
            }
        }

        private ArticleGridColumns AddArticleDetails(ArticleGridColumns articleDetails)
        {
            ProgrammaProgramatoriArticle article;

            var articleDetailId = articleDetails.PPArticleID ?? 0;

            article = new ProgrammaProgramatoriArticle
            {
                Id = articleDetailId,
                MODEL = articleDetails.Model,
                DATA_ARRIVO_SCHEDE_PR = articleDetails.DataArrivoSchedePr,
                CONF_CAMP = articleDetails.ConfCamp,
                NR_MACH = articleDetails.NrMach,
                DATA_ENTRATA_IN_PROD = articleDetails.DataEntrataInProd,
                DATA_ARRIVO_SCHEMA = articleDetails.DataArrivoSchema,
                DATA_INIZIO_SVIL_TG_BASE = articleDetails.DataInizioSvilTgBase,
                DATA_FINE_SVIL_TG_BASE = articleDetails.DataFineSvilTgBase,
                GG1 = articleDetails.Gg1,
                DATA_ARRIVO_SCHEDE = articleDetails.DataArrivoSchede,
                DATA_ARRIVO_DISCO = articleDetails.DataArrivoDisco,
                GG2 = articleDetails.Gg2,
                GG3 = articleDetails.Gg3,
                DATA_INIZIO_PROD = articleDetails.DataInizioProd,
                GG4 = articleDetails.Gg4,
                TOT_GG = articleDetails.TotGg,
                INIZ_FINE = articleDetails.Finish,
                PESI_X_ITA = articleDetails.Weights,
                TEMP = articleDetails.Time,
                CONF_PP = articleDetails.ConfPp,
                ArticleID = articleDetails.Num,
                Finished = articleDetails.Finished
            };

            Task.Run(async () =>
                {
                    if (article.Id == 0)
                    {
                        article = await _ppArticleService.Create(article);
                        articleDetails.PPArticleID = article.Id;
                    }
                    else
                    {
                        article = await _ppArticleService.Update(article.Id, article);
                    }
                });

            return articleDetails;
        }

        private static ObservableCollection<JobType> CreateLabels()
        {
            ObservableCollection<JobType> result = new ObservableCollection<JobType>();
            int count = JobsTypes.Length;

            for (int i = 0; i < count; i++)
            {
                JobType label = new JobType
                {
                    JobTypeID = i,
                    Color = JobsColorTypes[i].ToString(),
                    JobTypeName = JobsTypes[i]
                };
                result.Add(label);
            }
            return result;
        }

        private static ObservableCollection<TaskStatus> CreateStates()
        {
            ObservableCollection<TaskStatus> result = new ObservableCollection<TaskStatus>();
            int count = States.Length;

            for (int i = 0; i < count; i++)
            {
                TaskStatus label = new TaskStatus
                {
                    Id = i,
                    StatusBrush = BrushStates[i],
                    Caption = States[i]
                };
                result.Add(label);
            }
            return result;
        }

        private ProgrammerTask _programmerTask;
        public ProgrammerTask ProgrammerTask
        {
            get => _programmerTask;
            set
            {
                _programmerTask = value;
                OnPropertyChanged(nameof(ProgrammerTask));

            }
        }

        public void OnChangedTask(int articleId)
        {
            var gridRow = _ppArticles.FirstOrDefault(a => a.Num == articleId);

            if (gridRow?.Num != null)
            {
                var articleRow = Task.Run(() => GetArticleRow((int) gridRow.Num)).Result;

                if (articleRow != null)
                {
                    _ppArticles.Remove(gridRow);
                    _ppArticles.Add(articleRow);
                    _ppArticles = new ObservableCollection<ArticleGridColumns>(_ppArticles.OrderBy(n => n.Num));
                    OnPropertyChanged(nameof(PpArticles));
                    OnPropertyChanged(nameof(ProgrammerTasks));
                }
            }
        }

        private async Task<ArticleGridColumns> GetArticleRow(int id)
        {
            return await _articleService.GetArticleRow(id);
        }

        private ObservableCollection<ProgrammerTask> _programmerTasks;
        public ObservableCollection<ProgrammerTask> ProgrammerTasks
        {
            get => _programmerTasks;
            set
            {
                _programmerTasks = value;
                OnPropertyChanged(nameof(ProgrammerTasks));
            }
        }

        private IEnumerable<Angajati> _knittingProgrammers;
        public IEnumerable<Angajati> KnittingProgrammers
        {
            get => _knittingProgrammers;
            set
            {
                _knittingProgrammers = value;
                OnPropertyChanged(nameof(KnittingProgrammers));
            }
        }

        private ObservableCollection<Articole> _articles;
        public ObservableCollection<Articole> Articles
        {
            get => _articles;
            set
            {

                _articles = value;
                OnPropertyChanged(nameof(Articles));
            }
        }

        private ObservableCollection<ArticleGridColumns> _ppArticles;

        public ObservableCollection<ArticleGridColumns> PpArticles
        {
            get => _ppArticles;
            set
            {
                _ppArticles = value;
                OnPropertyChanged(nameof(PpArticles));
            }
        }

        public ICommand SaveTaskCommand { get; set; }
        public ICommand ChangeTaskCommand { get; set; }
        public ICommand DeleteTaskCommand { get; set; }


        public bool GetTasks()
        {
            Task.Run(async () =>
            {
                _programmerTasks = new ObservableCollection<ProgrammerTask>();

                IEnumerable<Articole> articles = await _articleService.GetAll();
                IEnumerable<ProgrammerTask> programerTasks = await _taskService.GetAll();

                var tasks = articles.SelectMany(a => programerTasks, (a, p) => new { a, p })
                    .Where(t => t.p.ArticleID == t.a.Id)
                    .Select(t => new ProgrammerTask
                    {
                        ProgrammerTaskID = t.p.ProgrammerTaskID,
                        StartTask = t.p.StartTask,
                        EndTask = t.p.EndTask,
                        Note = t.p.Note,
                        TaskCompleted = t.p.TaskCompleted,
                        ProgrammerID = t.p.ProgrammerID,
                        ArticleID = t.a.Id,
                        JobTypeID = t.p.JobTypeID,
                        ArticleTitle = t.a.Articol
                    });

                foreach (var item in tasks)
                {
                    _programmerTasks.Add(item);
                }
                return true;
            });

            return false;
        }
        public void GetArticles()

        {
            Task.Run(async () =>
            {
                _articles = new ObservableCollection<Articole>();
                _ppArticles = new ObservableCollection<ArticleGridColumns>();

                IEnumerable<Articole> articles = await _articleService.GetAll();
                IEnumerable<ProgrammerTask> programerTasks = await _taskService.GetAll();
                IEnumerable<ProgrammaProgramatoriArticle> ppArticles = await _ppArticleService.GetAll();

                var programmerTasks = programerTasks as ProgrammerTask[] ?? programerTasks.ToArray();
                var ppValues = ppArticles as ProgrammaProgramatoriArticle[] ?? ppArticles.ToArray();
                var articoles = articles as Articole[] ?? articles.ToArray();

                var mergedData =
                    from a in articoles
                    select new ArticleGridColumns
                    {
                        PPArticleID = ppValues.FirstOrDefault(id => id.ArticleID == a.Id)?.Id,
                        Num = a.Id,
                        Articolo = a.Articol,
                        Model = ppValues.FirstOrDefault(i => i.ArticleID == a.Id)?.MODEL,
                        Fin = a.Finete,

                        ProgrammerPR = programmerTasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 1)?.Programmer.Angajat,
                        DataArrivoSchedePr = ppValues.FirstOrDefault(id => id.ArticleID == a.Id)?.DATA_ARRIVO_SCHEDE_PR,
                        StartPr = programmerTasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 1)?.StartTask,
                        EndPr = programmerTasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 1)?.EndTask,

                        ProgrammerCa = programmerTasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 2)?.Programmer.Angajat,
                        StartCa = programmerTasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 2)?.StartTask,
                        EndCa = programmerTasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 2)?.EndTask,

                        ConfCamp = ppValues.FirstOrDefault(i => i.ArticleID == a.Id)?.CONF_CAMP,
                        DataArrivoFilo = ppValues.FirstOrDefault(i => i.ArticleID == a.Id)?.DATA_ARRIVO_FILO,
                        CapiPrevisti = ppValues.FirstOrDefault(i => i.ArticleID == a.Id)?.CAPI_PREVISTI,
                        NrMach = ppValues.FirstOrDefault(i => i.ArticleID == a.Id)?.NR_MACH,
                        DataEntrataInProd = ppValues.FirstOrDefault(i => i.ArticleID == a.Id)?.DATA_ENTRATA_IN_PROD,
                        DataArrivoSchema = ppValues.FirstOrDefault(i => i.ArticleID == a.Id)?.DATA_ARRIVO_SCHEMA,
                        DataInizioSvilTgBase = ppValues.FirstOrDefault(i => i.ArticleID == a.Id)?.DATA_INIZIO_SVIL_TG_BASE,
                        DataFineSvilTgBase = ppValues.FirstOrDefault(i => i.ArticleID == a.Id)?.DATA_FINE_SVIL_TG_BASE,
                        Gg1 = ppValues.FirstOrDefault(i => i.ArticleID == a.Id)?.GG1,
                        DataArrivoSchede = ppValues.FirstOrDefault(i => i.ArticleID == a.Id)?.DATA_ARRIVO_SCHEDE,
                        DataArrivoDisco = ppValues.FirstOrDefault(i => i.ArticleID == a.Id)?.DATA_ARRIVO_DISCO,

                        ProgrammerPP = programmerTasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 3)?.Programmer.Angajat,
                        StartPP = programmerTasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 3)?.StartTask,
                        EndPP = programmerTasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 3)?.EndTask,

                        Gg2 = ppValues.FirstOrDefault(i => i.ArticleID == a.Id)?.GG2,
                        Ok = "OK",
                        Gg3 = ppValues.FirstOrDefault(i => i.ArticleID == a.Id)?.GG3,

                        ProgrammerSv = programmerTasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 4)?.Programmer.Angajat,
                        StartSv = programmerTasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 4)?.StartTask,
                        EndSv = programmerTasks.FirstOrDefault(i => i.ArticleID == a.Id && i.JobTypeID == 4)?.EndTask,

                        DataInizioProd = ppValues.FirstOrDefault(i => i.ArticleID == a.Id)?.DATA_INIZIO_PROD,
                        Gg4 = ppValues.FirstOrDefault(i => i.ArticleID == a.Id)?.GG4,
                        TotGg = ppValues.FirstOrDefault(i => i.ArticleID == a.Id)?.TOT_GG,
                        Finish = ppValues.FirstOrDefault(i => i.ArticleID == a.Id)?.INIZ_FINE,
                        Weights = ppValues.FirstOrDefault(i => i.ArticleID == a.Id)?.PESI_X_ITA,
                        Time = ppValues.FirstOrDefault(i => i.ArticleID == a.Id)?.TEMP,
                        ConfPp = ppValues.FirstOrDefault(i => i.ArticleID == a.Id)?.CONF_PP,
                        Note = a.Note,
                        Finished = ppValues.FirstOrDefault(i => i.ArticleID == a.Id)?.Finished
                    };


                foreach (var article in mergedData)
                {
                    _ppArticles.Add(article);
                }

                foreach (var articole in articoles)
                {
                    _articles.Add(articole);
                }
            });
        }
        private void GetEmployees()
        {
            Task.Run(async () => { _knittingProgrammers = await _employeeService.GetProgrammers(); });
        }
    }
}
