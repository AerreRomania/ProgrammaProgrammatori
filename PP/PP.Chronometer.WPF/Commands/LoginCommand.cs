using System;
using System.Threading.Tasks;
using PP.Chronometer.WPF.State.Authenticators;
using PP.Chronometer.WPF.State.Navigator;
using PP.Chronometer.WPF.ViewModels;
using PP.Domain.Exceptions;

namespace PP.Chronometer.WPF.Commands
{
    public class LoginCommand : AsyncCommandBase
    {
        private readonly LoginViewModel _loginViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;

        public LoginCommand(LoginViewModel loginViewModel,IAuthenticator authenticator,  IRenavigator renavigator)
        {
            _authenticator = authenticator;
            _renavigator = renavigator;
            _loginViewModel = loginViewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _loginViewModel.ErrorMessage = string.Empty;
            try
            {
                await _authenticator.Login(_loginViewModel.Username, (string)parameter);
                _renavigator.Renavigate();
            }
            catch (UserNotFoundException)
            {
                _loginViewModel.ErrorMessage = "Username does not exist.";
            }
            catch (InvalidPasswordException)
            {
                _loginViewModel.ErrorMessage = "Incorrect password.";
            }
            catch (Exception)
            {
                _loginViewModel.ErrorMessage = "Login failed.";
            }
        }


    }
}
