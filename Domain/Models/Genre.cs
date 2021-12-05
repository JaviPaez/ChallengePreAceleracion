using System.Collections.Generic;

namespace Domain.Models
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}