using Microsoft.EntityFrameworkCore;
using WaCore.Contracts.Data;

namespace WaCore.Data
{
    public class WacDbContext : DbContext, IWacDbContext
    {

    }
}
