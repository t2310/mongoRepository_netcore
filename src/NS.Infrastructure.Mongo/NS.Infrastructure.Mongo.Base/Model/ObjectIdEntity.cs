using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NS.Infrastructure.Mongo.Base.Model
{
    public abstract class ObjectIdEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
