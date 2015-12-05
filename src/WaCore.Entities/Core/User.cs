using WaCore.Contracts.Entities.Core;

namespace WaCore.Entities.Core
{
    public class User : IUser
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string PlainPassword { get; set; }

        public string HashedPassword { get; set; }
    }
}
