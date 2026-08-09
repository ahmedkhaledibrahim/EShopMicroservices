using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Users.Domain.Entities.Common
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid ID { get; private set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
    }
}
