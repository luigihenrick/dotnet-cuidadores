using System;

namespace Cuidadores.Core.Entities
{
    public class Visita
    {
        public long Id { get; set; }
        public DateTime? Criado { get; set; }
        public DateTime? Atualizado { get; set; }
        public long CuidadorId { get; set; }
        public long PacienteId { get; set; }
        public DateTime? DataVisita { get; set; }
        public StatusVisita StatusVisita { get; set; }
        public string Justificativa { get; set; }
    }
}
