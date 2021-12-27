using Entities.Models;
using System;
using System.Collections.Generic;

namespace Entities.DTO.Incoming
{
    public class MovieDTO : BaseEntityDTO
    {
        public byte[] Image { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public int Score { get; set; }

        public Genre Genre { get; set; }
        public ICollection<Character> Characters { get; set; }
    }
}