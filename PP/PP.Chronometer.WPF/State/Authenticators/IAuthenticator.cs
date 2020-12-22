using System.Threading.Tasks;
using PP.Domain.Models;

namespace PP.Chronometer.WPF.State.Authenticators
{
    public interface IAuthenticator
    {
        Angajati CurrentUser { get; }
        bool IsLoggedIn { get; }
        Task Login(string username, string password);
        void Logout();
    }
}
