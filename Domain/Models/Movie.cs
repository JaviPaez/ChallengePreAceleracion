using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Movie : BaseEntity
    {
        public byte[] Image { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public int Score { get; set; }

        public ICollection<Character> Characters { get; set; }
    }
}