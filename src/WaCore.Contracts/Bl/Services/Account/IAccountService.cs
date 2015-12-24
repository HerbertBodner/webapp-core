using System.Threading.Tasks;
using WaCore.Contracts.Entities.Core;

namespace WaCore.Contracts.Bl.Services.Account
{
    public interface IAccountService
    {
        Task<ILoginResult> Login(string userName, string password, bool rememberMe);
        Task<IAccountResult> Register(IUser user, string password);
        Task SignOutAsync();
    }
}
