using System.ComponentModel.DataAnnotations;

namespace Cuidadores.Core.Entities
{
    public enum TipoPessoa
    {
        [Display(Name = "Cuidador")]
        Cuidador,
        [Display(Name = "Paciente")]
        Paciente
    }
}
