using System;

namespace RocheApp.Domain.Services.User
{
    public class UserCreatorResult
    {
        public Guid UserId { get; set; }
        public byte[] RowVersion { get; set; }
    }
}