using System;
using System.Threading.Tasks;
using System.Windows;
using PP.Chronometer.WPF.ViewModels;
using PP.Domain.Services;

namespace PP.Chronometer.WPF.Commands
{
    public class StopAssignedJobCommand : AsyncCommandBase
    {
        private readonly ChronometerViewModel _viewModel;
        private readonly IProgrammerJobService _programmerJobService;

        public StopAssignedJobCommand(ChronometerViewModel viewModel, IProgrammerJobService programmerJobService)
        {
            _viewModel = viewModel;
            _programmerJobService = programmerJobService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.ErrorMessage = string.Empty;
            _viewModel.StatusMessage = string.Empty;

            try
            {
                if (_viewModel.StartedJob == null)
                {
                    _viewModel.ButtonStart = true;
                    Application.Current.MainWindow?.Show();
                }
                else if (_viewModel.StartedJob.ProgrammerProgressID != 0 && _viewModel.StartedJob.EndWork == null)
                {
                    _viewModel.StartedJob.EndWork = DateTime.Now;
                    await _programmerJobService.Update(_viewModel.StartedJob.ProgrammerProgressID, _viewModel.StartedJob);
                    _viewModel.ButtonStart = true;
                    Application.Current.MainWindow?.Show();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
    }
}