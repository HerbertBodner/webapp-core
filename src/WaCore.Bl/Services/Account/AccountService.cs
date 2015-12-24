using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using WaCore.Contracts.Bl.Services.Account;
using WaCore.Contracts.Entities.Core;
using WaCore.Entities.Core;

namespace WaCore.Bl.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AccountService(SignInManager<User> signInManager, 
            UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<ILoginResult> Login(string userName, string password, bool rememberMe)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(userName, password, rememberMe, lockoutOnFailure: false);
            return new LoginResult
            {
                IsLockedOut = signInResult.IsLockedOut,
                IsNotAllowed = signInResult.IsNotAllowed,
                RequiresTwoFactor = signInResult.RequiresTwoFactor,
                Succeeded = signInResult.Succeeded
            };
        }

        public async Task<IAccountResult> Register(IUser user, string password)
        {
            var u = (User)user;
            var result = await _userManager.CreateAsync(u, password);
            if (!result.Succeeded)
            {
                return new AccountResult(result.Errors.Select(x => x.Description).ToList());
            }

            // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
            // Send an email with this link
            //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
            //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
            //    "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
            await _signInManager.SignInAsync(u, isPersistent: false);
            return AccountResult.Success();
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
