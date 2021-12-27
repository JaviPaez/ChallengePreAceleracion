using System;

namespace Entities.DTO.Incoming
{
    public class BaseEntityDTO
    {
        public int Id { get; set; }
        public DateTime AddedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdateDate { get; set; }
    }
}