using System.Collections.Generic;

namespace WebApplication.Models
{
    public class Character
    {
        public int CharacterId { get; set; }
        public byte[] Image { get; set; }
        public string Name { get; set; }       
        public int Age { get; set; }
        public decimal Weight { get; set; }
        public string Story { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}