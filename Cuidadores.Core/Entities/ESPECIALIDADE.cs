using System;

namespace Cuidadores.Core.Entities
{
    public class Especialidade
    {
        public long Id { get; set; }
        public DateTime? Criado { get; set; }
        public DateTime? Atualizado { get; set; }
        public string Descricao { get; set; }
    }
}
