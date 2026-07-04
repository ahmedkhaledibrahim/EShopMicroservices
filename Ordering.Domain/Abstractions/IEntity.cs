namespace Ordering.Domain.Abstractions
{
    public interface IEntity<T>
    {
        public T ID { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public string? CreatedBy { get; set; }
    }
}
