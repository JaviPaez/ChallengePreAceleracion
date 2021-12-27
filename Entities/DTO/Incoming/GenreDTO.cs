namespace Entities.DTO.Incoming
{
    public class GenreDTO : BaseEntityDTO
    {
        public string Name { get; set; }
        public byte[] Image { get; set; }
    }
}