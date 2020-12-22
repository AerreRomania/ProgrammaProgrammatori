using PP.Domain.Services;
using PP.WPF.ViewModels;
using System.Threading.Tasks;

namespace PP.WPF.Commands
{
    public class DeleteTaskCommand : AsyncCommandBase
    {
        private HomeViewModel _viewModel;
        private ITaskService _taskService;

        public DeleteTaskCommand(HomeViewModel viewModel, ITaskService taskService)
        {
            _viewModel = viewModel;
            _taskService = taskService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var task = _viewModel.ProgrammerTask;
            if (task != null)
            {
                await _taskService.Delete(task.ProgrammerTaskID);
                _viewModel.ProgrammerTask.ProgrammerTaskID = task.ProgrammerTaskID;
                _viewModel.OnChangedTask(task.ArticleID);
            }
        }
    }
}