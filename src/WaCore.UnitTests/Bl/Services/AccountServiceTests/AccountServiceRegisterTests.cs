using NSubstitute;
using WaCore.Bl.Services;
using WaCore.Contracts.Bl.Services;
using WaCore.Contracts.Entities.Core;
using WaCore.Contracts.Enums;
using WaCore.Contracts.Exceptions.AuthenticationExceptions;
using WaCore.Contracts.Data.Repositories;
using Xunit;

namespace WaCore.UnitTests.Bl.Services.AccountServiceTests
{
    public class AccountServiceRegisterTests
    {
        protected IAccountService<IUser> GetAccountService(IUserRepository<IUser> userRepoFake = null, IPasswordService passwordServiceFake = null)
        {
            userRepoFake = userRepoFake ?? Substitute.For<IUserRepository<IUser>>();
            passwordServiceFake = passwordServiceFake ?? Substitute.For<IPasswordService>();
            return new AccountService<IUser>(userRepoFake, passwordServiceFake);
        }

        protected IUser GetUserFake(string email, string password, string name = "")
        {
            var user = Substitute.For<IUser>();
            user.Email = email;
            user.PlainPassword = password;
            user.Name = name;
            return user;
        }

        [Fact]
        public void Register_WithEmptyEmail_Throws()
        {
            var service = GetAccountService();
            var user = GetUserFake("", "password");
            Assert.Throws<UserEmailOrPasswordNullException>(() => service.Register(user));
        }

        [Fact]
        public void Register_WithEmptyPassword_Throws()
        {
            var service = GetAccountService();
            var user = GetUserFake("my@email.com", "");
            Assert.Throws<UserEmailOrPasswordNullException>(() => service.Register(user));
        }

        [Fact]
        public void Register_WithWeakPassword_Throws()
        {
            var userRepoFake = Substitute.For<IUserRepository<IUser>>();
            userRepoFake.FindByEmail("").ReturnsForAnyArgs((IUser)null);
            var service = GetAccountService(userRepoFake);
            var user = GetUserFake("my@email.com", "weakPassword");
            Assert.Throws<PasswordNotStrongEnoughException>(() => service.Register(user));
        }

        [Fact]
        public void Register_WithExistingEmail_Throws()
        {
            var user = GetUserFake("my@email.com", "weakPassword");
            var userRepoFake = Substitute.For<IUserRepository<IUser>>();
            userRepoFake.FindByEmail("my@email.com").Returns(user);

            var passwordServiceFake = Substitute.For<IPasswordService>();
            passwordServiceFake.CheckStrength("weakPassword").Returns(PasswordScore.Weak);
            
            var service = GetAccountService(userRepoFake);

            Assert.Throws<UserWithEmailAlreadyExistsException>(() => service.Register(user));
        }

        [Fact]
        public void Register_WithValidEmailAndPassword_CallsRepositorySave()
        {
            var userRepoFake = Substitute.For<IUserRepository<IUser>>();
            userRepoFake.FindByEmail("my@email.com").Returns((IUser)null);

            var passwordServiceFake = Substitute.For<IPasswordService>();
            passwordServiceFake.CheckStrength("MyStrongPassword1").Returns(PasswordScore.Strong);

            var service = GetAccountService(userRepoFake, passwordServiceFake);
            var user = GetUserFake("my@email.com", "MyStrongPassword1");

            service.Register(user);
            userRepoFake.Received().Save(user);
        }
    }
}
