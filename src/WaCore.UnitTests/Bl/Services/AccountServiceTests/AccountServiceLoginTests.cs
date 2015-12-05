using NSubstitute;
using WaCore.Bl.Services;
using WaCore.Contracts.Bl.Services;
using WaCore.Contracts.Entities.Core;
using WaCore.Contracts.Exceptions;
using WaCore.Contracts.Data.Repositories;
using Xunit;

namespace WaCore.UnitTests.Bl.Services.AccountServiceTests
{
    public class AccountServiceLoginTests
    {
        protected IAccountService<IUser> GetAccountService(IUserRepository<IUser> userRepoFake = null, IPasswordService passwordServiceFake = null)
        {
            userRepoFake = userRepoFake ?? Substitute.For<IUserRepository<IUser>>();
            passwordServiceFake = passwordServiceFake ?? Substitute.For<IPasswordService>();
            return new AccountService<IUser>(userRepoFake, passwordServiceFake);
        }

        [Fact]
        public void Login_WithEmptyEmail_Throws()
        {
            var service = GetAccountService();
            Assert.Throws<UserEmailOrPasswordNullException>(() => service.Login("", "pwd"));
        }

        [Fact]
        public void Login_WithEmptyPassword_Throws()
        {
            var service = GetAccountService();
            Assert.Throws<UserEmailOrPasswordNullException>(() => service.Login("my@mail.com", ""));
        }

        [Fact]
        public void Login_WithInvalidEmail_Throws()
        {
            var userRepoFake = Substitute.For<IUserRepository<IUser>>();
            userRepoFake.FindByEmail("my@mail.com").Returns((IUser)null);

            var service = GetAccountService(userRepoFake);
            Assert.Throws<UserNotFoundException>(() => service.Login("my@mail.com", "pwd"));
        }

        [Fact]
        public void Login_WithInvalidPassword_Throws()
        {
            var userFake = Substitute.For<IUser>();
            userFake.Email = "my@mail.com";
            userFake.Password = "MyVerySecurePassword123#";
            userFake.Name = "MyName";

            var userRepoFake = Substitute.For<IUserRepository<IUser>>();
            userRepoFake.FindByEmail("my@mail.com").Returns(userFake);

            var passwordServiceFake = Substitute.For<IPasswordService>();
            passwordServiceFake.ValidatePassword(Arg.Any<string>(), Arg.Any<string>()).Returns(false);

            var service = GetAccountService(userRepoFake, passwordServiceFake);
            Assert.Throws<InvalidPasswordException>(() => service.Login("my@mail.com", "wrongPwd"));
        }

        [Fact]
        public void Login_WithValidEmailAndPassword_Throws()
        {
            var userFake = Substitute.For<IUser>();
            userFake.Email = "my@mail.com";
            userFake.Password = "MyVerySecurePassword123#";
            userFake.Name = "MyName";

            var userRepoFake = Substitute.For<IUserRepository<IUser>>();
            userRepoFake.FindByEmail("my@mail.com").Returns(userFake);

            var passwordServiceFake = Substitute.For<IPasswordService>();
            passwordServiceFake.ValidatePassword(Arg.Any<string>(), Arg.Any<string>()).Returns(true);

            var service = GetAccountService(userRepoFake, passwordServiceFake);
            Assert.Equal(userFake, service.Login("my@mail.com", "MyVerySecurePassword123#"));
        }

    }
}
