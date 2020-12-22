using System.Linq;
using PP.Domain.Services;
using PP.WPF.ViewModels;
using System.Threading.Tasks;

namespace PP.WPF.Commands
{
    public class SaveTaskCommand : AsyncCommandBase
    {
        private HomeViewModel _viewModel;
        private ITaskService _taskService;

        public SaveTaskCommand(HomeViewModel viewModel, ITaskService taskService)
        {
            this._viewModel = viewModel;
            _taskService = taskService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var task = _viewModel.ProgrammerTask;

            if (task.ProgrammerTaskID == 0)
            {
                var createdTask = await _taskService.Create(task);

                var programmerTask = _viewModel.ProgrammerTasks.FirstOrDefault(p => 
                                                                       p.ArticleTitle == task.ArticleTitle &&
                                                                       p.ProgrammerID == task.ProgrammerID &&
                                                                       p.StartTask == task.StartTask &&
                                                                       p.EndTask == task.EndTask);
                if (programmerTask != null)
                {
                    programmerTask.ProgrammerTaskID = createdTask.ProgrammerTaskID;
                }

                _viewModel.OnChangedTask(createdTask.ArticleID);
            }
        }
    }
}
