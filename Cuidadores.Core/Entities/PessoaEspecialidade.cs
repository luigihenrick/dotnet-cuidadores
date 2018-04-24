using System;

namespace Cuidadores.Core.Entities
{
    public class PessoaEspecialidade
    {
        public long Id { get; set; }
        public DateTime? Criado { get; set; }
        public DateTime? Atualizado { get; set; }
        public long? IdPessoa { get; set; }
        public long? IdEspecialidade { get; set; }
    }
}
