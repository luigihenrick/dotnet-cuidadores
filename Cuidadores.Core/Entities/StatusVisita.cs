using System.ComponentModel.DataAnnotations;

namespace Cuidadores.Core.Entities
{
    public enum StatusVisita
    {
        [Display(Name = "Pendente")]
        Pendente,
        [Display(Name = "Confirmada")]
        Confirmada,
        [Display(Name = "Realizada")]
        Realizada,
        [Display(Name = "Cancelada")]
        Cancelada
    }
}
