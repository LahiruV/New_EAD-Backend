using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Web.Model
{

    public class TicketInsertRequest
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        [Required]
        public string Train { get; set; } 
        [Required]
        public string Name { get; set; }
        [Required]
        public string NIC { get; set; }
        [Required]
        public string PhoneNo { get; set; }
        [Required]
        public string ReservationDate { get; set; }
        public string CreateDate { get; set; }
        public int SeatNumber { get; set; }
        public bool IsActive { get; set; }
    }

    public class GetAllTicketResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<TicketInsertRequest> data { get; set; }

    }

    public class GetTicketByNICResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public TicketInsertRequest data { get; set; }
    }

    public class DeleteTicketRequest
    {
        [Required]
        public string NIC { get; set; }
    }

}
