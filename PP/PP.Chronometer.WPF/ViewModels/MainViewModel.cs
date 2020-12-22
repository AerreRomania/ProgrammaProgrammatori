using System.Windows.Input;
using PP.Chronometer.WPF.Commands;
using PP.Chronometer.WPF.State.Authenticators;
using PP.Chronometer.WPF.State.Navigator;
using PP.Chronometer.WPF.ViewModels.Factories;

namespace PP.Chronometer.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public INavigator Navigator { get; set; }

        public IAuthenticator Authenticator { get; }
        public ICommand UpdateCurrentViewModelCommand { get; }
        public ViewModelBase CurrentViewModel => Navigator.CurrentViewModel;

        public MainViewModel(IChronoViewModelFactory viewModelFactory, INavigator navigator, IAuthenticator authenticator)
        {
            Navigator = navigator;
            Authenticator = authenticator;
            Navigator.StateChanged += Navigator_StateChanged;
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, viewModelFactory);

            UpdateCurrentViewModelCommand.Execute(ViewType.Login);
        }

        private void Navigator_StateChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
