namespace Entities.Models
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }
        public byte[] Image { get; set; }
    }
}