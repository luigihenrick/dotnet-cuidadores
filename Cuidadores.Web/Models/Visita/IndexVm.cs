using System.Collections.Generic;
using Cuidadores.Core.Entities;
using Cuidadores.Core.Services;
using Cuidadores.Core.Models;

namespace Cuidadores.Web.Models.Visita
{
    public class IndexVm
    {
        public IEnumerable<VisitaVm> Visitas { get; set; }

        public TipoPessoa? TipoPessoa { get; set; }

        public IndexVm()
        {
            Visitas = new List<VisitaVm>();
        }

        public IndexVm(long pessoaId, TipoPessoa? tipoPessoa)
        {
            Visitas = new List<VisitaVm>();

            TipoPessoa = tipoPessoa;

            if (tipoPessoa == null)
            {
                return;
            }
            
            if (tipoPessoa == Core.Entities.TipoPessoa.Cuidador)
            {
                Visitas = VisitaService.Instance.FindVisitasVm(
                    @"select * from v_visita where CuidadorId = @CuidadorId order by Criado desc",
                    new { CuidadorId = pessoaId });    
            }
            else if (tipoPessoa == Core.Entities.TipoPessoa.Paciente)
            {
                Visitas = VisitaService.Instance.FindVisitasVm(
                    @"select * from v_visita where PacienteId = @PacienteId order by Criado desc",
                    new {PacienteId = pessoaId});
            }
            
        }
    }
}