using PP.Chronometer.WPF.ViewModels;
using PP.Domain.Models;
using PP.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PP.Chronometer.WPF.Commands
{
    public class SaveNoteCommand : AsyncCommandBase
    {
        private readonly AssignedTasksViewModel _viewModel;
        private readonly ITaskService _taskService;

        public SaveNoteCommand(AssignedTasksViewModel viewModel, ITaskService programmerJobService)
        {
            _viewModel = viewModel;
            _taskService = programmerJobService;
        }

        public override async Task ExecuteAsync(object parameter)
        {
           

            try
            { var selected = _viewModel.SelectedRow;
                ProgrammerTask programmerTask = await _taskService.Get(selected.ProgrammerTaskID);
                programmerTask.Note = selected.Note;
                    await _taskService.Update(_viewModel.SelectedRow.ProgrammerTaskID,programmerTask);
                    
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
    }
}
