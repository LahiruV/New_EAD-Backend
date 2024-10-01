using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class UserInsertRequest
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string NIC { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string Mail { get; set; }
        [Required]
        public string PhoneNo { get; set; }
        [Required]
        public string Password { get; set; }
        public string JoinDate { get; set; }

    }

    public class GetAllUserResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<UserInsertRequest> data { get; set; }

    }

    public class TravellerInsertRequest 
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string NIC { get; set; }
        [Required]
        public string Mail { get; set; }
        [Required]
        public string PhoneNo { get; set; }
        [Required]
        public string Password { get; set; }
        public string JoinDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class GetAllTravellerResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<TravellerInsertRequest> data { get; set; }

    }

    public class GetTravellerByNICResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public TravellerInsertRequest data { get; set; }
    }

    public class DeleteTravellerRequest
    {
        [Required]
        public string NIC { get; set; }
    }
    public class DeleteTravellerResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

    }
}
