using System;
using System.Collections.Generic;

namespace RocheApp.Domain.Models
{
    public class User
    {
        public Guid UserId { get; set; } = Guid.NewGuid();

        public int ExperiencePoints { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte PetsDeleted { get; set; }
        public byte Status { get; set; }
        public byte[] RowVersion { get; set; }

        public ICollection<Pet> Pets { get; set; }
    }
}
