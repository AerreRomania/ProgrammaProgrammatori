using System.Threading.Tasks;
using PP.Chronometer.WPF.ViewModels;
using PP.Chronometer.WPF.Views;
using PP.Domain.Services;

namespace PP.Chronometer.WPF.Commands
{
    public class OpenChronometerForAssignedCommand : AsyncCommandBase
    {
        private AssistanceTasksViewModel _viewModel;
        private IProgrammerJobService _programmerJobService;

        public OpenChronometerForAssignedCommand(IProgrammerJobService programmerJobService, AssistanceTasksViewModel viewModel)
        {
            _programmerJobService = programmerJobService;
            _viewModel = viewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            ChronometerView chronometer = new ChronometerView
            {
                DataContext = new ChronometerViewModel(_programmerJobService)
            };

            ((ChronometerViewModel)chronometer.DataContext).SelectedRow = _viewModel.SelectedRow;
            
            chronometer.Show();
        }
    }
}
