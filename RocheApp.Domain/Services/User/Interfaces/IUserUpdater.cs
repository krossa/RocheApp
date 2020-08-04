using System.Collections.Generic;

namespace RocheApp.Domain.Services.User.Interfaces
{
    public interface IUserUpdater
    {
        IEnumerable<UserUpdateResult> Update(int count);
    }
}