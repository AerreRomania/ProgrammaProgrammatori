﻿
using DevExpress.Mvvm;
using NLog;
using PP.Domain.Models;
using PP.Domain.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace PP.WPF.ViewModels
{
    public class TrackingViewModel : ViewModelBase
    {
        Logger log = LogManager.GetCurrentClassLogger();
        private readonly IArticleService _articleService;
        private readonly IEmployeeService _employeeService;
        private readonly IProgrammerJobService _programmerJobService;
        public DelegateCommand<object> RefreshCommand { get; set; }

        public TrackingViewModel(IArticleService articleService, IEmployeeService employeeService, IProgrammerJobService programmerJobService)
        {
            _articleService = articleService;
            _employeeService = employeeService;
            _programmerJobService = programmerJobService;
            RefreshCommand = new DelegateCommand<object>(OnRefreshCommand);
            Labels = CreateLabels();
            Statuses = CreateStates();
           // GetProgrammers();
            GetData();
            log.Info("TRACKING STARTED");
        }

        private void OnRefreshCommand(object obj)
        {
            GetData();
        }

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

        public static string[] States = { "Computer", "Computer/Machine", "Machine" };
        public static Brush[] BrushStates = { new LinearGradientBrush(Colors.MediumSpringGreen, Colors.Yellow, 45.0), new LinearGradientBrush(Colors.MediumBlue, Colors.Yellow, 45.0), new LinearGradientBrush(Colors.MediumPurple, Colors.Yellow, 45.0) };

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

        private ProgrammerProgress _programmerProgress;

        public ProgrammerProgress ProgrammerProgress
        {
            get => _programmerProgress;
            set
            {
                _programmerProgress = value;
                OnPropertyChanged(nameof(ProgrammerProgress));
            }
        }

        private ObservableCollection<ProgrammerProgress> _programmerProgresses;

        public ObservableCollection<ProgrammerProgress> ProgrammerProgresses
        {
            get => _programmerProgresses;
            set
            {
                _programmerProgresses = value;
                OnPropertyChanged(nameof(ProgrammerProgresses));
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

        private async Task<IEnumerable<Angajati>> GetEmployees()
        {
            try
            {
                  return await _employeeService.GetProgrammers();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return null;
            }
        }
        private void GetProgrammers()
        {
            try
            {
                Task.Run(async () =>
               { _knittingProgrammers = await GetEmployees(); });

            }
            catch(Exception ex)
            {
                log.Error(ex);
            }
           
        }
        private async void GetData()
        {
            try
            {
               // Task.Run(async () => { _knittingProgrammers = await _employeeService.GetProgrammers(); });
               await Task.Run(async () =>
                {
                    _knittingProgrammers = await GetEmployees();
                    OnPropertyChanged("KnittingProgrammers");
                    //GetProgrammers();


                    _programmerProgresses = new ObservableCollection<ProgrammerProgress>();
                    var programmerProgresses = await _programmerJobService.GetAll();
                    var articles = await _articleService.GetAll();
                    foreach (var programmerProgress in programmerProgresses)
                    {
                        programmerProgress.ArticleTitle = articles.FirstOrDefault(id => id.Id == programmerProgress.Progress.ArticleID)?.Articol;
                        if (programmerProgress.EndWork == null) programmerProgress.EndWork = programmerProgress.StartWork.AddHours(1);
                        _programmerProgresses.Add(programmerProgress);
                    }
                    OnPropertyChanged("ProgrammerProgresses");
                });
            }

            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
    }
}