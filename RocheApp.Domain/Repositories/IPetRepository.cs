using System;
using System.Collections.Generic;

namespace RocheApp.Domain.Repositories
{
    public interface IPetRepository
    {
        void Delete(Guid userId, IEnumerable<Guid> petIds);
    }
}