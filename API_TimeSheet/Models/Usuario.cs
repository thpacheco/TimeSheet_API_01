using MongoDB.Bson;

namespace API_TimeSheet.Models
{
    public class Usuario
    {
        public ObjectId Id { get; internal set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public string Senha { get; set; }

        public string DataCadastro { get; set; }

        public bool Status { get; set; }
    }
}