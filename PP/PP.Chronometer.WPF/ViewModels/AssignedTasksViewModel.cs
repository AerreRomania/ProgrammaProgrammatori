using NLog;
using PP.Chronometer.WPF.Commands;
using PP.Chronometer.WPF.State.Authenticators;
using PP.Domain.Columns;
using PP.Domain.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PP.Chronometer.WPF.ViewModels
{
    public class AssignedTasksViewModel : ViewModelBase
    {
        private readonly IAuthenticator _authenticator;
        private readonly ITaskService _taskService;
        Logger log = LogManager.GetCurrentClassLogger();
        public AssignedTasksViewModel(IAuthenticator authenticator, ITaskService taskService, IProgrammerJobService programmerJobService)
        {
            _authenticator = authenticator;
            _taskService = taskService;
            GetAssignedTasks();
            OpenChronometerCommand = new OpenChronometerCommand(programmerJobService, this);
        }

        private ObservableCollection<ProgrammerGridColumns> _programmerTasks;

        public ObservableCollection<ProgrammerGridColumns> ProgrammerTasks
        {
            get => _programmerTasks;
            set
            {
                _programmerTasks = value;
                OnPropertyChanged(nameof(ProgrammerTasks));
            }
        }

        private ObservableCollection<ProgrammerGridColumns> _finishedProgrammerTasks;

        public ObservableCollection<ProgrammerGridColumns> FinishedProgrammerTasks
        {
            get => _finishedProgrammerTasks;
            set
            {
                _finishedProgrammerTasks = value;
                OnPropertyChanged(nameof(FinishedProgrammerTasks));
            }
        }

        public ProgrammerGridColumns SelectedRow { get; set; }

        public ICommand OpenChronometerCommand
        {
            get;
            set;
        }

        private void GetAssignedTasks()
        {
            try
            {
                _programmerTasks = new ObservableCollection<ProgrammerGridColumns>();
                _finishedProgrammerTasks = new ObservableCollection<ProgrammerGridColumns>();

                Task.Run(async () =>
                {
                    var tasks = await _taskService.GetAssigned(_authenticator.CurrentUser.Id);
                    foreach (var task in tasks)
                    {
                        _programmerTasks.Add(task);
                    }

                    var finishedTasks = await _taskService.GetAssigned(_authenticator.CurrentUser.Id, true);
                    foreach (var finishedTask in finishedTasks)
                    {
                        _finishedProgrammerTasks.Add(finishedTask);
                    }
                });
            }
            catch(Exception ex)
            {
                log.Error(ex);
            }
        }
    }
}