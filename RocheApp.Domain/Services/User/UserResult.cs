using System.Collections.Generic;

namespace RocheApp.Domain.Services.User
{
    public class UserResult
    {
        public int TotalUserCount { get; set; } = 0;
        public int TotalPetCount { get; set; } = 0;
        public IEnumerable<Models.User> Users { get; set; }
    }
}