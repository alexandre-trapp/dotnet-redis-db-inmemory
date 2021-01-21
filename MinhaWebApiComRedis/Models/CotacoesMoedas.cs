using System;

namespace MinhaWebApiComRedis.Models
{
    public class CotacoesMoedas
    {
        public Guid Id { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public string SiglaMoeda { get; set; }
        public string NomeMoeda { get; set; }
        public string Mensagem { get; set; }
    }
}
