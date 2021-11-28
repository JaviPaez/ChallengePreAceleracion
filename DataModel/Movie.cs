using System;
using System.Collections.Generic;

namespace WebApplication.DataModel
{
    public class Movie
    {
        public int MovieId { get; set; }
        public byte[] Image { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public int Score { get; set; }

        public ICollection<Character> Characters { get; set; }
    }
}