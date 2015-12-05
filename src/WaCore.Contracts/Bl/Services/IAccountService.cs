using WaCore.Contracts.Entities.Core;

namespace WaCore.Contracts.Bl.Services
{
    public interface IAccountService<TUser>
        where TUser : IUser
    {
        void Register(TUser user);
        TUser SaveNewNonExistingUser(TUser user);
        TUser Login(string email, string password);
        void ChangePassword(string email, string oldPassword, string newPassword);
        bool ResetPasswort(IUser user, string newPasswort, string newPasswordConfirmation);
    }
}
