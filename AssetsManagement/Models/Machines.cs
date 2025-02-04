using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace AssetsManagement.Models
{
    public class Machines
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? _id { get; set; }
        [Required]
        public string Name { get; set; }
        public IDictionary<string, string> Assets { get; set; }

    }
}
