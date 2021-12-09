using System.Collections.Generic;

namespace Domain.Models
{
    public class Character : BaseEntity
    {
        public byte[] Image { get; set; }
        public string Name { get; set; }       
        public int Age { get; set; }
        public decimal Weight { get; set; }
        public string Story { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}