﻿using MongoDB.Bson;

namespace API_TimeSheet.Models
{
    public class Apontamento
    {
        public ObjectId Id { get; set; }

        public string IdUsuario { get; set; }

        public string Entrada { get; set; }

        public string SaidaAlmoco { get; set; }

        public string RetornoAlmoco { get; set; }

        public string Saida { get; set; }

        public string DataMarcacao { get; set; }

        public string HorasTotalDia { get; set; }

        public string DescricaoAtividade { get; set; }

        public string CodigoAtividade { get; set; }
    }
}