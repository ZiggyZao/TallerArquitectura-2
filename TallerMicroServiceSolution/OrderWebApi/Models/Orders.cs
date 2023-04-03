using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OrderWebApi.Models
{
    [Serializable, BsonIgnoreExtraElements]
    public class Orders
    {
        [BsonId, BsonElement("_id"),BsonRepresentation(BsonType.ObjectId)]
        public string OrderId { get; set; }

        [BsonElement("_customer_id"), BsonRepresentation(BsonType.Int32)]
        public int CustomerId { get; set; }

        [BsonElement("_ordered_on"), BsonRepresentation(BsonType.DateTime)]
        public DateTime OrderedOn { get; set; }

        [BsonElement("order_details")]
        public List<OrderDetail> OrderDetails { get; set; }  
    }
}
