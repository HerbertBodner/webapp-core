namespace WaCore.Contracts.Entities.Core
{
    public interface IUser
    {
        string Email { get; set; }
        string PlainPassword { get; set; }
        string HashedPassword { get; set; }
        string Name { get; set; }
    }
}
