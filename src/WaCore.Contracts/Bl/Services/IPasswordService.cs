using WaCore.Contracts.Enums;

namespace WaCore.Contracts.Bl.Services
{
    public interface IPasswordService
    {
        PasswordScore CheckStrength(string password);
        string HashPassword(string password);
        bool ValidatePassword(string password, string correctHash);
    }
}
