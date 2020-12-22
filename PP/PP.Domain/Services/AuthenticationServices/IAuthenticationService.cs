using System.Threading.Tasks;
using PP.Domain.Models;

namespace PP.Domain.Services.AuthenticationServices
{
    public interface IAuthenticationService 
    {
        Task<Angajati> Login(string username, string password);
        Task<bool> Register(string username, string password, string confirmPassword);

    }
}
