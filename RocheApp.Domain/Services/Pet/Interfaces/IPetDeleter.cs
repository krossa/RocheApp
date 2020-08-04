using System.Threading.Tasks;

namespace RocheApp.Domain.Services.Pet.Interfaces
{
    public interface IPetDeleter
    {
        Task DeleteAsync(Models.User user);
    }
}