using System.Collections.Generic;
using System.Web.Mvc;
using Cuidadores.Core.Entities;
using Cuidadores.Core.Services;

namespace Cuidadores.Web.Controllers
{
    public class EspecialidadeController : BaseController
    {
        public ActionResult Index()
        {
            IEnumerable<Especialidade> especialidades = EspecialidadeService.Instance.All();
            
            return View(especialidades);
        }

        [HttpGet]
        public ActionResult Editar(long /* especialidadeId */ id)
        {
            Especialidade especialidade;
            
            if (id != 0)
            {
                especialidade = EspecialidadeService.Instance.GetEspecialidade(id);
            }
            else
            {
               especialidade = new Especialidade(){Id = 0};
            }

            return View(especialidade);
        }

        [HttpPost]
        public ActionResult Editar(Especialidade model)
        {
            if (model.Id == 0)
            {
                EspecialidadeService.Instance.Create(model);
            }
            else
            {
                EspecialidadeService.Instance.Update(model);
            }

            AddMessageSuccess("Especialidade salva com sucesso.");

            return RedirectToAction("Index", "Especialidade");
        }

        [HttpPost]
        public JsonResult Excluir(long /* especialidadeId */ id)
        {
            bool remove = EspecialidadeService.Instance.Delete(id);

            if (remove == false)
            {
                AddMessageError("Falha ao remover especialidade.");
                return Json(new {success = false, error = "Não foi possível remover especialidade."});
            }

            AddMessageSuccess("Especialidade removida com sucesso.");

            return Json(new {success = true});
        }
    }
}
