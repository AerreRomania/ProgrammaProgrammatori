using PP.Domain.Models;
using System.Threading.Tasks;

namespace PP.Domain.Services.AuthenticationServices
{
    public interface IAuthenticationService
    {
        Task<Angajati> Login(string username, string password);

        Task<bool> Register(string username, string password, string confirmPassword);
    }
}