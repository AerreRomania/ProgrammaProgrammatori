using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using PP.Domain.Services;
using PP.WPF.Commands;
using PP.WPF.State.Authenticators;
using PP.WPF.State.Navigator;

namespace PP.WPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {

        private readonly IEmployeeService _employeeService;

        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public MessageViewModel ErrorMessageViewModel { get; }

        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }

        private IEnumerable<string> _usernames;
        public IEnumerable<string> Usernames
        {
            get => _usernames;
            set
            {
                _usernames = value;
                OnPropertyChanged(nameof(Usernames));
            }
        }



        public ICommand LoginCommand { get; }

        public LoginViewModel(IAuthenticator authenticator, IEmployeeService employeeService, IRenavigator renavigator)
        {
            _employeeService = employeeService;
            ErrorMessageViewModel = new MessageViewModel();
            LoginCommand = new LoginCommand(this, authenticator,renavigator);
            GetEmployees();
        }

        private void GetEmployees()
        {
            _usernames = new List<string>();

            Task.Run(async () =>
            {
                var users = await _employeeService.GetManagers();
                _usernames = users.Select(a => a.Angajat);
            });
        }
    }
}
