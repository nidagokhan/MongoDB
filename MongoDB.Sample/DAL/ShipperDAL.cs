using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Sample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDB.Sample.DAL
{
    public class ShipperDAL
    {
        private readonly IMongoCollection<Shipper> mongoCollection;

        public ShipperDAL(string mongoDbConnectionString, string dbName, string collectionName)
        {
            var client = new MongoClient(mongoDbConnectionString);
            var database = client.GetDatabase(dbName);
            mongoCollection = database.GetCollection<Shipper>(collectionName);
        }

        public List<Shipper> GetAll()
        {
            return mongoCollection.Find(book => true).ToList();
        }

        public Shipper GetById(string id)
        {
            var docId = new ObjectId(id);
            return mongoCollection.Find(a => a.Id == docId).FirstOrDefault();
        }

        public Shipper Add(Shipper shipper)
        {
            mongoCollection.InsertOne(shipper);
            return shipper;
        }

        public void Update(string id,Shipper shipper)
        {
            var docId= new ObjectId(id);
            mongoCollection.ReplaceOne(a => a.Id == docId, shipper);
        }
        public void Delete(string id)
        {
            var docId = new ObjectId(id);
            mongoCollection.DeleteOne(a => a.Id == docId);
        }
    }
}
