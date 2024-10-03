﻿using MongoDB.Bson.Serialization.Attributes;

namespace Web.Model
{
    public class Inventory
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string InventoryId { get; set; }  

        public string ProductID { get; set; }
        public int StockLevel { get; set; }
        public int LowStockThreshold { get; set; }
        public bool StockAlert { get; set; }
    }
}
