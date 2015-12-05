namespace WaCore.Contracts.Entities.Core
{
    public interface IUser
    {
        string Email { get; set; }
        string Password { get; set; }
        string Name { get; set; }
    }
}
