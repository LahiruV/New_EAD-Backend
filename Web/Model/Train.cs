using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Web.Model
{

    public class TrainInsertRequest
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        [Required]
        public string GenerateID { get; set; }
        [Required]
        public string StartPlace { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        public string StartTime { get; set; }
        [Required] 
        public string ArriveTime { get; set; }
        [Required]
        public string Price { get; set; }
        [Required]
        public int NoOfSeats { get; set; }
    }

    public class GetAllTrainResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<TrainInsertRequest> data { get; set; }

    }

    public class GetTrainByIDResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public TrainInsertRequest data { get; set; }
    }

    public class DeleteTrainRequest
    {
        [Required]
        public string GenerateID { get; set; }
    }

}
