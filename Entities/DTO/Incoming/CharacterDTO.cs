using Entities.Models;
using System.Collections.Generic;

namespace Entities.DTO.Incoming
{
    public class CharacterDTO
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Weight { get; set; }
        public string Story { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}