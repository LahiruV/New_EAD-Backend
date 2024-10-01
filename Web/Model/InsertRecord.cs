using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Web.Model
{
    public class InsertRecordRequest
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        public string CreateDate { get; set; }
        public string UpdateDate { get; set; }
        [Required]
        [BsonElement(elementName: "Name")]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Contact { get; set; }
        public double Salary { get; set; }
    }

    public class InsertRecordResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    public class GetAllRecordResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<InsertRecordRequest> data { get; set; }

    }
    public class GetRecordByIDResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public InsertRecordRequest data { get; set; }
    }
    public class GetRecordByNameResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<InsertRecordRequest> data { get; set; }
    }
    public class UpdateRecordByIDResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

    } 
    public class UpdateSalaryByIdRequest
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public int Salary { get; set; }

    } 
    public class UpdateSalaryByIdResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

    }   
    public class DeleteRecordByIDRequest
    {
        [Required]
        public string Id { get; set; }  
    }   
    public class DeleteRecordByIDResponse
    {        
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

    }   
    public class DeleteAllRecordResponse
    {        
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

    }
}