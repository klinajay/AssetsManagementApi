using AssetsManagement.Models;
using MongoDB.Driver;

namespace AssetsManagement.DB
{
    public class DbContext
    {

        public IMongoDatabase database;
        public DbContext(IConfiguration configuration)
        {
            var client = new MongoClient("mongodb://localhost:3017/");
            database = client.GetDatabase("AssetsManagement");
        }
        public IMongoCollection<Machines> Machines => database.GetCollection<Machines>("Machines");
        public IMongoCollection<Assets> Assets => database.GetCollection<Assets>("Assets");
    }
}
