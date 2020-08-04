namespace RocheApp.Domain.Services.User.Interfaces
{
    public interface IUserService
    {
        UserResult Users(UserFilter filter);
    }
}