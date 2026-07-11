namespace Ordering.Domain.Abstractions
{
    public interface IEntity
    {
        public DateTimeOffset DateCreated { get; set; }
        public string? CreatedBy { get; set; }
    }
}
