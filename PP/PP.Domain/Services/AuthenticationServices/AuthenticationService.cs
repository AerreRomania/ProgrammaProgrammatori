using System.Threading.Tasks;
using PP.Domain.Exceptions;
using PP.Domain.Models;

namespace PP.Domain.Services.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IEmployeeService _employeeService;

        public AuthenticationService(IEmployeeService employeeService)
        {
            _employeeService = employeeService;

        }

        public async Task<Angajati> Login(string username, string password)
        {
       
            Angajati employee = await _employeeService.GetEmployeeByName(username);

            if (employee == null)
            {
                throw new UserNotFoundException(username);
            }

            if (employee.Angajat != username || employee.CodAngajat != password)
            {
                throw new InvalidPasswordException(username, password);
            }

            return employee;
        }

        public async Task<bool> Register(string username, string password, string confirmPassword)
        {
            bool success = false;
            return success;
        }

    }
}
