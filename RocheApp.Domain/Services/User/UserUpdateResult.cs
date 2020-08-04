namespace RocheApp.Domain.Services.User
{
    public class UserUpdateResult
    {
        public int ExperiencePoints { get; set; }
        public byte[] RowVersion { get; set; }
    }
}