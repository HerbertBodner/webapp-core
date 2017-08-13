namespace WaCore.Contracts.Data.Repositories.Base
{
    public interface IDbContextFactory
    {
        IDbContext GetContext();
    }
}
