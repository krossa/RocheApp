using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RocheApp.Domain.Repositories
{
    public interface IPetRepository
    {
        Task DeleteAsync(Guid userId, IEnumerable<Guid> petIds);
    }
}