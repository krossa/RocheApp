using RocheApp.Domain.Repositories;
using RocheApp.Domain.Services.Pet.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace RocheApp.Domain.Services.Pet
{
    public class PetDeleter : IPetDeleter
    {
        private readonly ApplicationSettings _settings;
        private readonly IPetRepository _petRepository;

        public PetDeleter(ApplicationSettings settings, IPetRepository petRepository)
        {
            _settings = settings;
            _petRepository = petRepository;
        }

        public async Task DeleteAsync(Models.User user)
        {
            if (!CanDelete(user)) return;

            var count = user.Pets.Count / 2;

            var petIdsToDelete = user.Pets.Take(count).Select(p => p.PetId);
            await _petRepository.DeleteAsync(user.UserId, petIdsToDelete);
        }

        private bool CanDelete(Models.User user) =>
            user.PetsDeleted == 0 && user.Pets.Any() &&
            user.ExperiencePoints > _settings.PointsThresholdForDeletingPets;

    }
}