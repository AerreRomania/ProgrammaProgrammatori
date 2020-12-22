using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using PP.Domain.Models;
using PP.Domain.Services.AuthenticationServices;

namespace PP.Chronometer.WPF.State.Authenticators
{
    public class Authenticator : IAuthenticator, INotifyPropertyChanged
    {
        private readonly IAuthenticationService _authenticationService;

        public Authenticator(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        private Angajati _currentUser;
        public Angajati CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(IsLoggedIn));
                OnPropertyChanged(nameof(CurrentUser));
            }
        }
  
        public bool IsLoggedIn => CurrentUser != null;

        public async  Task Login(string username, string password)
        {
            
                CurrentUser = await _authenticationService.Login(username, password);
        
        }

        public void Logout()
        {
            CurrentUser = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
