using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cuidadores.Core.Entities;
using Cuidadores.Core.Services;

namespace Cuidadores.Web.Models.Pessoa
{
    public class PessoaBuscarVm
    {
        public IEnumerable<Core.Entities.Pessoa> Pessoas { get; set; }
        public List<SelectListItem> EspecialidadesList { get; set; }

        public PessoaBuscarVm()
        {
            EspecialidadesList = new List<SelectListItem>();
            
            IEnumerable<Especialidade> especialidades = EspecialidadeService.Instance.All();

            EspecialidadesList.AddRange(especialidades.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Descricao
            }));
        }

        public PessoaBuscarVm(IEnumerable<Core.Entities.Pessoa> pessoas)
        {
            EspecialidadesList = new List<SelectListItem>();
            
            IEnumerable<Especialidade> especialidades = EspecialidadeService.Instance.All();

            EspecialidadesList.AddRange(especialidades.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Descricao
            }));

            Pessoas = pessoas;
        }
    }
}