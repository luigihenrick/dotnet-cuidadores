using Cuidadores.Core.Entities;

namespace Cuidadores.Core.Models
{
    public class VisitaVm:Visita
    {
        public string PacienteNome { get; set; }
        public string PacienteEmail { get; set; }
        public string PacienteTelefone { get; set; }
        public string PacienteEndereco { get; set; }
        
        public string CuidadorNome { get; set; }
        public string CuidadorEmail { get; set; }
        public string CuidadorTelefone { get; set; }
        public string CuidadorEndereco { get; set; }

        public VisitaVm()
        {
            
        }

    }
}