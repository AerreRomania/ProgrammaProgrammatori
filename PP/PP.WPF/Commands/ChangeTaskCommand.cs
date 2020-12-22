using PP.Domain.Services;
using PP.WPF.ViewModels;
using System.Threading.Tasks;

namespace PP.WPF.Commands
{
    public class ChangeTaskCommand : AsyncCommandBase
    {
        private HomeViewModel _viewModel;
        private ITaskService _taskService;
        public ChangeTaskCommand(HomeViewModel viewModel, ITaskService taskService)
        {
            this._viewModel = viewModel;
            _taskService = taskService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            var task = _viewModel.ProgrammerTask;
            if (task != null)
            {
                var t = await _taskService.Update(task.ProgrammerTaskID, task);
                _viewModel.OnChangedTask(task.ArticleID);
            }
        }
    }
}
