using PP.Chronometer.WPF.Commands;
using PP.Domain.Columns;
using PP.Domain.Models;
using PP.Domain.Services;
using System.Windows.Input;

namespace PP.Chronometer.WPF.ViewModels
{
    public class ChronometerViewModel : ViewModelBase
    {
        public ChronometerViewModel(IProgrammerJobService programmerJobService)
        {
            StartJobCommand = new StartAssignedJobCommand(this, programmerJobService);
            StopJobCommand = new StopAssignedJobCommand(this, programmerJobService);
            ErrorMessageViewModel = new MessageViewModel();
            StatusMessageViewModel = new MessageViewModel();
        }

        public ProgrammerGridColumns SelectedRow { get; set; } = new ProgrammerGridColumns();

        public ProgrammerProgress StartedJob { get; set; }

        public bool ButtonComputer { get; set; }
        public bool ButtonComputerMachine { get; set; }
        public bool ButtonMachine { get; set; }

        private bool _buttonStart = true;

        public bool ButtonStart
        {
            get => _buttonStart;
            set
            {
                _buttonStart = value;
                OnPropertyChanged(nameof(ButtonStart));
            }
        }

        public MessageViewModel StatusMessageViewModel { get; }

        public string StatusMessage
        {
            set => StatusMessageViewModel.Message = value;
        }

        public MessageViewModel ErrorMessageViewModel { get; }

        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }

        public ICommand StartJobCommand
        {
            get;
            set;
        }

        public ICommand StopJobCommand
        {
            get;
            set;
        }
    }
}