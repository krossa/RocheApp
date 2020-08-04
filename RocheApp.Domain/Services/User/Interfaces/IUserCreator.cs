namespace RocheApp.Domain.Services.User.Interfaces
{
    public interface IUserCreator
    {
        UserCreatorResult Create(Models.User user);
    }
}