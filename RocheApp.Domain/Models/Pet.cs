using System;

namespace RocheApp.Domain.Models
{
    public class Pet
    {
        public Guid PetId { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }

        public string Name { get; set; }
        public byte[] TimeStampValue { get; set; }
    }
}