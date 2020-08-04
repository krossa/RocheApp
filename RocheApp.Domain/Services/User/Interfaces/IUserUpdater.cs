using System.Collections.Generic;

namespace RocheApp.Domain.Services.User.Interfaces
{
    public interface IUserUpdater
    {
        IAsyncEnumerable<UserUpdateResult> UpdateAsync(int count);
    }
}