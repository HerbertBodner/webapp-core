using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WaCore.Bl.Services;
using WaCore.Contracts.Bl.Services;
using WaCore.Contracts.Data.Repositories;
using WaCore.Contracts.Entities.Core;
using WaCore.Contracts.Enums;
using WaCore.Contracts.Exceptions.AuthenticationExceptions;
using Xunit;

namespace WaCore.UnitTests.Bl.Services.AccountServiceTests
{
    public class AccountServiceChangePasswordTests
    {
        protected IAccountServiceOld<IUser> GetAccountService(IUserRepository<IUser> userRepoFake = null, IPasswordService passwordServiceFake = null)
        {
            userRepoFake = userRepoFake ?? Substitute.For<IUserRepository<IUser>>();
            passwordServiceFake = passwordServiceFake ?? Substitute.For<IPasswordService>();
            return new AccountServiceOld<IUser>(userRepoFake, passwordServiceFake);
        }

        [Fact]
        public void ChangePassword_EmptyEmail_Throws()
        {
            var service = GetAccountService();
            Assert.Throws<UserEmailOrPasswordNullException>(() => service.ChangePassword("", "oldpwd", "newPwd"));
        }

        [Fact]
        public void ChangePassword_EmptyNewPassword_Throws()
        {
            var service = GetAccountService();
            Assert.Throws<UserEmailOrPasswordNullException>(() => service.ChangePassword("my@mail.com", "oldpwd", ""));
        }

        [Fact]
        public void ChangePassword_WithInvalidEmail_Throws()
        {
            var userRepoFake = Substitute.For<IUserRepository<IUser>>();
            userRepoFake.FindByEmail("my@mail.com").Returns((IUser)null);

            var service = GetAccountService(userRepoFake);
            Assert.Throws<UserNotFoundException>(() => service.ChangePassword("my@mail.com", "oldpwd", "newPwd"));
        }

        [Fact]
        public void ChangePassword_WithWrongOldPassword_Throws()
        {
            var userRepoFake = Substitute.For<IUserRepository<IUser>>();

            var passwordServiceFake = Substitute.For<IPasswordService>();
            passwordServiceFake.ValidatePassword(Arg.Any<string>(), Arg.Any<string>()).Returns(false);

            var service = GetAccountService(userRepoFake, passwordServiceFake);
            Assert.Throws<InvalidPasswordException>(() => service.ChangePassword("my@mail.com", "oldpwd", "newPwd"));
        }

        [Fact]
        public void ChangePassword_WithWeakNewPassword_Throws()
        {
            var userRepoFake = Substitute.For<IUserRepository<IUser>>();

            var passwordServiceFake = Substitute.For<IPasswordService>();
            passwordServiceFake.ValidatePassword(Arg.Any<string>(), Arg.Any<string>()).Returns(true);
            passwordServiceFake.CheckStrength(Arg.Any<string>()).Returns(PasswordScore.Weak);

            var service = GetAccountService(userRepoFake, passwordServiceFake);
            Assert.Throws<PasswordNotStrongEnoughException>(() => service.ChangePassword("my@mail.com", "oldpwd", "newPwd"));
        }

        [Fact]
        public void ChangePassword_WithStrongNewPassword_CallsHashPassword()
        {
            var userRepoFake = Substitute.For<IUserRepository<IUser>>();

            var passwordServiceFake = Substitute.For<IPasswordService>();
            passwordServiceFake.ValidatePassword(Arg.Any<string>(), Arg.Any<string>()).Returns(true);
            passwordServiceFake.CheckStrength(Arg.Any<string>()).Returns(PasswordScore.Strong);

            var service = GetAccountService(userRepoFake, passwordServiceFake);
            service.ChangePassword("my@mail.com", "oldpwd", "strongNewPwd");

            passwordServiceFake.Received().HashPassword("strongNewPwd");
        }
    }
}
