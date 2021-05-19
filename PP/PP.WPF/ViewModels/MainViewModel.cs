using NLog;
using PP.WPF.Commands;
using PP.WPF.State.Authenticators;
using PP.WPF.State.Navigator;
using PP.WPF.ViewModels.Factories;
using System.Windows.Input;

namespace PP.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        Logger Log = LogManager.GetCurrentClassLogger();
        public INavigator Navigator { get; set; }

        public IAuthenticator Authenticator { get; }
        public ICommand UpdateCurrentViewModelCommand { get; }
        public ViewModelBase CurrentViewModel => Navigator.CurrentViewModel;

        public MainViewModel(IPPViewModelFactory viewModelFactory, INavigator navigator, IAuthenticator authenticator)
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