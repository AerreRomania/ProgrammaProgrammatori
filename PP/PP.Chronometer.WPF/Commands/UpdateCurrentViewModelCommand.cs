using System;
using System.Windows.Input;
using PP.Chronometer.WPF.State.Navigator;
using PP.Chronometer.WPF.ViewModels.Factories;

namespace PP.Chronometer.WPF.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly INavigator _navigator;
        private readonly IChronoViewModelFactory _viewModelFactory;

        public UpdateCurrentViewModelCommand(INavigator navigator, IChronoViewModelFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is ViewType viewType)
            {
                _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);
            }
        }
    }
}
