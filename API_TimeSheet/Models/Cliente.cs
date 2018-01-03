using MongoDB.Bson;

namespace API_TimeSheet.Models
{
    public class Cliente
    {
        public ObjectId id { get; set; }
        public string Nome { get; set; }
    }
}