namespace Domain.DTOs
{
    public abstract class BaseDTO
    {
        public Guid Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}