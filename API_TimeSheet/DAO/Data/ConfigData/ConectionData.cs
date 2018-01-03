using MongoDB.Driver;

namespace API_TimeSheet.DAO.Data.ConfigData
{
    public class ConectionData
    {

        public MongoDatabase AbriConection()
        {
            string stringConection = "mongodb://AntaresTecnologia:Msv46l74HwALXr01@cluster0-shard-00-00-ktmlj.mongodb.net:27017,cluster0-shard-00-01-ktmlj.mongodb.net:27017,cluster0-shard-00-02-ktmlj.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin";
            MongoClient client = new MongoClient(stringConection);
            var server = client.GetServer();
            return server.GetDatabase("thiagopacheco");
        }
    }
}