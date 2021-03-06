using System;

namespace Entities.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        //public int Status { get; set; } = 1;
        public DateTime AddedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdateDate { get; set; }
    }
}