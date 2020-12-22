using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using PP.Chronometer.WPF.Commands;
using PP.Chronometer.WPF.State.Authenticators;
using PP.Domain.Columns;
using PP.Domain.Services;

namespace PP.Chronometer.WPF.ViewModels
{
    public class AssistanceTasksViewModel : ViewModelBase
    {
        private readonly IAuthenticator _authenticator;
        private readonly ITaskService _taskService;

        public AssistanceTasksViewModel(IAuthenticator authenticator, ITaskService taskService, IProgrammerJobService programmerJobService)
        {
            _authenticator = authenticator;
            _taskService = taskService;
            GetAssignedTasks();
            OpenChronometerCommand = new OpenChronometerForAssignedCommand(programmerJobService, this);
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
            _programmerTasks = new ObservableCollection<ProgrammerGridColumns>();
            _finishedProgrammerTasks = new ObservableCollection<ProgrammerGridColumns>();

            Task.Run(async () =>
            {
                var tasks = await _taskService.GetAllAssigned(_authenticator.CurrentUser.Id);
                foreach (var task in tasks)
                {
                    _programmerTasks.Add(task);
                }

              
            });
            Task.Run(async () =>
            {
             

                var finishedTasks = await _taskService.GetAllAssigned(_authenticator.CurrentUser.Id,true);
                foreach (var finishedTask in finishedTasks)
                {
                    _finishedProgrammerTasks.Add(finishedTask);
                }
            });
        }
    }
}
