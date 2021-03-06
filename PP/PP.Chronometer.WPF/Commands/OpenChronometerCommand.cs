﻿using PP.Chronometer.WPF.ViewModels;
using PP.Chronometer.WPF.Views;
using PP.Domain.Services;
using System.Threading.Tasks;

namespace PP.Chronometer.WPF.Commands
{
    public class OpenChronometerCommand : AsyncCommandBase
    {
        private AssignedTasksViewModel _viewModel;
        private IProgrammerJobService _programmerJobService;

        public OpenChronometerCommand(IProgrammerJobService programmerJobService, AssignedTasksViewModel viewModel)
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