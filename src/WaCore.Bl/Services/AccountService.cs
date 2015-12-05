using WaCore.Contracts.Bl.Services;
using WaCore.Contracts.Entities.Core;
using WaCore.Contracts.Enums;
using WaCore.Contracts.Exceptions.AuthenticationExceptions;
using WaCore.Contracts.Data.Repositories;

namespace WaCore.Bl.Services
{
    public class AccountService<TUser> : IAccountService<TUser>
        where TUser : IUser
    {
        IUserRepository<TUser> userRepo;
        IPasswordService passwordService;
        public AccountService(IUserRepository<TUser> userRepo, IPasswordService passwordService)
        {
            this.userRepo = userRepo;
            this.passwordService = passwordService;
        }

        public virtual void Register(TUser user)
        {
            if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.PlainPassword))
            {
                throw new UserEmailOrPasswordNullException();
            }

            var existingUser = userRepo.FindByEmail(user.Email);
            if (existingUser != null)
            {
                throw new UserWithEmailAlreadyExistsException(user.Email);
            }

            var strength = passwordService.CheckStrength(user.PlainPassword);
            if (strength < PasswordScore.Medium)
            {
                throw new PasswordNotStrongEnoughException(strength);
            }

            user.HashedPassword = passwordService.HashPassword(user.PlainPassword);

            SaveNewNonExistingUser(user);
        }

        public virtual TUser SaveNewNonExistingUser(TUser user)
        {
            return userRepo.Save(user);
        }

        public virtual TUser Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                throw new UserEmailOrPasswordNullException();
            }
            var existingUser = userRepo.FindByEmail(email);
            if (existingUser == null)
            {
                throw new UserNotFoundException(email);
            }

            if (!passwordService.ValidatePassword(password, existingUser.HashedPassword))
            {
                throw new InvalidPasswordException(password);
            }
            return existingUser;
        }

        public void ChangePassword(string email, string oldPassword, string newPassword)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(newPassword))
            {
                throw new UserEmailOrPasswordNullException();
            }

            var existingUser = userRepo.FindByEmail(email);
            if (existingUser == null)
            {
                throw new UserNotFoundException(email);
            }

            if (!passwordService.ValidatePassword(oldPassword, existingUser.HashedPassword))
            {
                throw new InvalidPasswordException(oldPassword);
            }

            var strength = passwordService.CheckStrength(newPassword);
            if (strength < PasswordScore.Medium)
            {
                throw new PasswordNotStrongEnoughException(strength);
            }

            existingUser.HashedPassword = passwordService.HashPassword(newPassword);
        }

        public bool ResetPasswort(IUser user, string newPasswort, string newPasswordConfirmation)
        {
            //TODO
            return false;
        }
    }
}
