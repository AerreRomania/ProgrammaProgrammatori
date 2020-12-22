using System;
using System.Threading.Tasks;
using PP.Chronometer.WPF.ViewModels;
using PP.Domain.Models;
using PP.Domain.Services;

namespace PP.Chronometer.WPF.Commands
{
    public class StartAssignedJobCommand : AsyncCommandBase
    {
        private readonly ChronometerViewModel _viewModel;
        private readonly IProgrammerJobService _programmerJobService;

        public StartAssignedJobCommand(ChronometerViewModel viewModel, IProgrammerJobService programmerJobService)
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
                    ProgrammerProgress programerJob = new ProgrammerProgress();

                    if (_viewModel.ButtonComputer)
                    {
                        programerJob.WorkLocationID = 1;
                    }
                    else if (_viewModel.ButtonComputerMachine)
                    {
                        programerJob.WorkLocationID = 2;
                    }
                    else if (_viewModel.ButtonMachine)
                    {
                        programerJob.WorkLocationID = 3;
                    }
                    else
                    {
                        _viewModel.ErrorMessage = "Select job place";
                    }

                    if (programerJob.WorkLocationID != 0)
                    {
                        programerJob.ProgrammerTaskID = _viewModel.SelectedRow.ProgrammerTaskID;
                        programerJob.StartWork = DateTime.Now;
                        programerJob.Repair = _viewModel.SelectedRow.Repair;
                        programerJob.Assistance = _viewModel.SelectedRow.Assistance;
                        _viewModel.StartedJob = await _programmerJobService.Create(programerJob);
                        _viewModel.ButtonStart = false;
                        _viewModel.StatusMessage = $"Task {_viewModel.SelectedRow.ArticleHeader} started successfully";
                  
                    }
                }
                else if (_viewModel.StartedJob.ProgrammerProgressID != 0 && _viewModel.StartedJob.EndWork == null)
                {
                    _viewModel.ErrorMessage = "Finish current job";
             
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
