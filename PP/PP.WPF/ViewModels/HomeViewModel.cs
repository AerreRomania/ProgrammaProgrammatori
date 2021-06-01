using DevExpress.Mvvm;
using NLog;
using PP.Domain.Columns;
using PP.Domain.Models;
using PP.Domain.Services;
using PP.WPF.Commands;
using System;
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
        Logger log = LogManager.GetCurrentClassLogger();
        private readonly IArticleService _articleService;
        private readonly IEmployeeService _employeeService;
        private readonly ITaskService _taskService;
        private readonly IArticleDetailsService _articleDetailsService;
        public DelegateCommand<object> RefreshCommand { get; set; }
        
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
            "Controcampione",
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
            Color.FromRgb(173, 216, 230),
            Color.FromRgb(169,169,169),
        };

        public static string[] States = { "In Progress", "Finished" };
        public static Brush[] BrushStates = { new LinearGradientBrush(Colors.Green, Colors.Yellow, 45.0), new SolidColorBrush(Colors.Red) };

        public HomeViewModel(IArticleService articleService,
                             IEmployeeService employeeService,
                             ITaskService taskService,
                              IArticleDetailsService articleDetailsService)
        {
            _articleService = articleService;
            _employeeService = employeeService;
            _taskService = taskService;
            _articleDetailsService = articleDetailsService;
         

            SaveTaskCommand = new SaveTaskCommand(this, taskService);
            ChangeTaskCommand = new ChangeTaskCommand(this, _taskService);
            DeleteTaskCommand = new DeleteTaskCommand(this, _taskService);
            InProgressCommand = new InProgressCommand(this, _articleService, _articleDetailsService, _taskService);
            ToDoCommand = new ToDoCommand(this, _articleService, _articleDetailsService, _taskService);
            FinishedCommand = new FinishedCommand(this, _articleService, _articleDetailsService, _taskService);
            AllCommand = new AllCommand(this, _articleService, _articleDetailsService, _taskService);
            RefreshCommand = new DelegateCommand<object>(OnRefreshCommand);
            LegendaFilterCommand = new DelegateCommand<string>(OnLegendFileter);

            Labels = CreateLabels();
            Statuses = CreateStates();
            GetEmployees();
            GetTasks();
            GetArticles();
            log.Info("TIMELINE STARTED");
        }

        private void OnLegendFileter(string obj)
        {
            
            switch (obj)
                {
                    case "Prototipo":

                        PpArticles = new ObservableCollection<ArticleGridColumns>(_originalpp.Where(x => x.ProgrammerPR != null));
                        OnPropertyChanged(nameof(PpArticles));
                        break;
                    case "Campionario":

                        PpArticles = new ObservableCollection<ArticleGridColumns>(_originalpp.Where(x => x.ProgrammerCa != null));
                        OnPropertyChanged(nameof(PpArticles));
                        break;
                    case "Preproduzione":

                        PpArticles = new ObservableCollection<ArticleGridColumns>(_originalpp.Where(x => x.ProgrammerPP != null));
                        OnPropertyChanged(nameof(PpArticles));
                        break;
                    case "Sviluppo":

                        PpArticles = new ObservableCollection<ArticleGridColumns>(_originalpp.Where(x => x.ProgrammerSvTg != null));
                        OnPropertyChanged(nameof(PpArticles));
                        break;
                    case "Controcampione":

                        PpArticles = new ObservableCollection<ArticleGridColumns>(_originalpp.Where(x => x.ProgrammerCo != null));
                        OnPropertyChanged(nameof(PpArticles));
                        break;

                }
           
        }

        private void OnRefreshCommand(object obj)
        {
            GetEmployees();
            GetTasks();
            GetArticles();
        }
        private string _filtername;
        public string FilterName
        {
            get => _filtername;
            set
            {
                _filtername = value;
                OnPropertyChanged(nameof(FilterName));
            }
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
            try
            {
                ArticleDetails article;

                var articleDetailId = articleDetails.ArticleDeatilsID ?? 0;

                article = new ArticleDetails
                {
                    Id = articleDetailId,
                    MachineNumber = articleDetails.MachineNumber,
                    CapiPrevisti = articleDetails.CapiPrevisti,
                    DataInizioProd = articleDetails.DataInizioProd,
                    Notes = articleDetails.Notes,
                    DataArrSchedePr = articleDetails.DataArrivoSchedePr,
                    DataConsegnaPr = articleDetails.DataConsegnaProt,
                    DataArrSchedeCa = articleDetails.DataArrSchedaCa,
                    DataConsegnaCa = articleDetails.DataConsegnaCa,
                    DataArrTagliaBase = articleDetails.DataArrivoTagliaBase,
                    DataArrInzioTagliaBase = articleDetails.DataArrivoInzioTagliaBase,
                    DataArrFineTagliaBase = articleDetails.DataArrivoFineTagliaBase,
                    DataArrSchedeCo = articleDetails.DataArrivoSchedaCo,
                    DataConsegnaCo = articleDetails.DataConsegnaCo,
                    DataArrSchedaDisco = articleDetails.DataArrivoSchedaDisco,
                    DiffGGProdData = (articleDetails.DataInizioProd-articleDetails.DataArrivoSchedaDisco).Value.TotalDays,
                    DiffGGProgData = (articleDetails.StartPP - articleDetails.DataArrivoSchedaDisco).Value.TotalDays,
                    DataConsegnaPP = articleDetails.DataConsegnaPP,
                    GG1 = articleDetails.GG1,
                    Ok = articleDetails.Ok,
                    DataFineSvilTgBase = articleDetails.DataFineSvilTgBase,
                    DataInizioSvilTgBase = articleDetails.DataInizioSvilTgBase,
                    GG2 = articleDetails.GG2,
                    Finish = articleDetails.Finish,
                    ArticleID = articleDetails.Num
                };

                Task.Run(async () =>
                    {
                        if (article.Id == 0)
                        {
                            article = await _articleDetailsService.Create(article);
                            articleDetails.ArticleDeatilsID = article.Id;
                        }
                        else
                        {
                            article = await _articleDetailsService.Update(article.Id, article);
                        }
                    });

                return articleDetails;
            }
            catch(Exception ex)
            {
                log.Error(ex);
                return null;
            }
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
            try { 
            var gridRow = _ppArticles.FirstOrDefault(a => a.Num == articleId);

            if (gridRow?.Num != null)
            {
                var articleRow = Task.Run(() => GetArticleRow((int)gridRow.Num)).Result;

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
            catch (Exception ex)
            {
                log.Error(ex);

            }
        }

        private async Task<ArticleGridColumns> GetArticleRow(int id)
        {
            try {
            return await _articleService.GetArticleRow(id);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return null;
            }
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
        private ObservableCollection<ArticleGridColumns> _originalpp;
        public ObservableCollection<ArticleGridColumns> OriginalPP
        {
            get => _originalpp;
            set
            {
                _originalpp = value;
                OnPropertyChanged(nameof(OriginalPP));
            }
        }

        public ICommand SaveTaskCommand { get; set; }
        public ICommand ChangeTaskCommand { get; set; }
        public ICommand DeleteTaskCommand { get; set; }

        public bool GetTasks()
        {
            try { 
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
                        Note = t.p.Note + "\n Time assigned: "+ (t.p.EndTask - t.p.StartTask).Days +" days, "+ (t.p.EndTask - t.p.StartTask).Hours+" hours and " + (t.p.EndTask - t.p.StartTask).Minutes+" minutes",
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
                OnPropertyChanged(nameof(ProgrammerTasks));
                return true;
            });
                return false;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return false;
            }
        }

        public ICommand InProgressCommand { get; set; }
        public ICommand ToDoCommand { get; set; }
        public ICommand FinishedCommand { get; set; }
        public ICommand AllCommand { get; set; }
        public ICommand LegendaFilterCommand { get; set; }

        public void GetArticles()

        {
            try { 
            Task.Run(async () =>
            {
                _articles = new ObservableCollection<Articole>();
                _ppArticles = new ObservableCollection<ArticleGridColumns>();

                IEnumerable<Articole> articles = await _articleService.GetAll();
                IEnumerable<ProgrammerTask> programerTasks = await _taskService.GetAll();
                IEnumerable<ArticleDetails> ppArticles = await _articleDetailsService.GetAll();

                var tasks = programerTasks as ProgrammerTask[] ?? programerTasks.ToArray();
                var articleDetails = ppArticles as ArticleDetails[] ?? ppArticles.ToArray();
                var articoles = articles as Articole[] ?? articles.ToArray();

                var mergedData =
                    from a in articoles
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
                int i = 1;
                mergedData = new ObservableCollection<ArticleGridColumns>(mergedData.OrderBy(n => n.DataInizioProd));
                foreach (var article in mergedData)
                {
                    article.NrCrt = i;
                    _ppArticles.Add(article);
                    i++;
                }

                foreach (var articole in articoles)
                {
                    _articles.Add(articole);
                }
               
                OnPropertyChanged(nameof(PpArticles));
                OriginalPP = PpArticles;
            });
               
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private void GetEmployees()
        {
            try { 
            Task.Run(async () => { _knittingProgrammers = await _employeeService.GetProgrammers();
                OnPropertyChanged(nameof(KnittingProgrammers));
                });
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
    }
}