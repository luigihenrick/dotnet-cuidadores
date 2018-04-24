using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cuidadores.Core.Entities;
using Cuidadores.Core.Models;
using Cuidadores.Core.Services;
using Cuidadores.Web.Models.Visita;

namespace Cuidadores.Web.Controllers
{
    public class VisitaController : BaseController
    {
        public ActionResult Index()
        {
            long id = CurrentUser.Id;
            TipoPessoa? tipoPessoa = CurrentUser.TipoPessoa;
            
            return View(new IndexVm(id, tipoPessoa));
        }

        [HttpGet]
        public ActionResult Nova(long /* cuidadorId */ id)
        {
            Pessoa pessoa = PessoaService.Instance.GetPessoa(id);

            if (pessoa == null)
            {
                AddMessageError("Pessoa não encontrada.");
                return RedirectToAction("Buscar", "Pessoa");
            }

            VisitaVm model = new VisitaVm
            {
                PacienteId = CurrentUser.Id,
                PacienteNome = CurrentUser.Nome,
                PacienteTelefone = CurrentUser.Telefone,
                PacienteEndereco = CurrentUser.Endereco + ", " + CurrentUser.Numero + " - " + CurrentUser.Bairro + " - " + CurrentUser.Cidade + " - " + CurrentUser.Uf,
                PacienteEmail = CurrentUser.Email,
                
                CuidadorId = pessoa.Id,
                CuidadorNome = pessoa.Nome,
                CuidadorTelefone = pessoa.Telefone,
                CuidadorEndereco = pessoa.Endereco + ", " + pessoa.Numero + " - " + pessoa.Bairro + " - " + pessoa.Cidade + " - " + pessoa.Uf,
                CuidadorEmail = pessoa.Email
            };

            return PartialView("_Nova", model);
        }

        [HttpPost]
        public ActionResult Nova(Visita model)
        {
            if (model == null)
            {
                AddMessageError("Informações não preenchidas.");
                return RedirectToAction("Buscar", "Pessoa");
            }

            if (model.DataVisita < DateTime.Now)
            {
                AddMessageError("Data de visita inválida.");
                return RedirectToAction("Buscar", "Pessoa");
            }
            
            Visita visita = VisitaService.Instance.Create(model);

            AddMessageSuccess("Visita adicionada com sucesso.");
            return RedirectToAction("Detalhes", "Visita", new {id = visita.Id});
        }

        public ActionResult Detalhes(long /* visitaId */id)
        {
            IEnumerable<VisitaVm> visitaVm = VisitaService.Instance.FindVisitasVm(
                @"select * from v_visita where Id = @Id",
                new {Id = id});

            return View(visitaVm.FirstOrDefault());
        }
        
        public ActionResult Confirmar(long /* visitaId */ id)
        {
            if (id == 0)
            {
                AddMessageError("Visita não encontrada.");
                return RedirectToAction("Index", "Visita");
            }

            Visita visita = VisitaService.Instance.GetVisita(id);

            if (visita == null)
            {
                AddMessageError("Visita não encontrada.");
                return RedirectToAction("Index", "Visita");
            }

            if (CurrentUser.Id != visita.CuidadorId
                && CurrentUser.Id != visita.PacienteId)
            {
                AddMessageError("Visita associada ao seu usuário.");
                return RedirectToAction("Index", "Visita");
            }

            visita.StatusVisita = StatusVisita.Confirmada;

            VisitaService.Instance.Update(visita);

            AddMessageSuccess("Visita confirmada.");

            return RedirectToAction("Index", "Visita");
        }

        [HttpGet]
        public ActionResult Cancelar(long /* visitaId */ id)
        {
            if (id == 0)
            {
                return Content("Visita não encontrada.");
            }

            VisitaVm visitaVm = VisitaService.Instance.FindVisitaVm(id);

            if (visitaVm == null)
            {
                return Content("Visita não encontrada.");
            }

            if (CurrentUser.Id != visitaVm.CuidadorId
                && CurrentUser.Id != visitaVm.PacienteId)
            {
                return Content("Visita associada ao seu usuário.");
            }

            return PartialView("_Cancelar", visitaVm);
        }

        [HttpPost]
        public ActionResult Cancelar(long /* visitaId */ id, string justificativa)
        {
            if (id == 0)
            {
                AddMessageError("Visita não encontrada.");
                return RedirectToAction("Index", "Visita");
            }

            Visita visita = VisitaService.Instance.GetVisita(id);

            if (visita == null)
            {
                AddMessageError("Visita não encontrada.");
                return RedirectToAction("Index", "Visita");
            }

            if (CurrentUser.Id != visita.CuidadorId 
                && CurrentUser.Id != visita.PacienteId)
            {
                AddMessageError("Visita associada ao seu usuário.");
                return RedirectToAction("Index", "Visita");
            }

            string justificativaNome = String.Empty;

            justificativaNome += CurrentUser.Nome + ": ";
            justificativaNome += justificativa;

            visita.StatusVisita = StatusVisita.Cancelada;
            visita.Justificativa = justificativaNome;

            VisitaService.Instance.Update(visita);

            AddMessageSuccess("Visita cancelada.");

            return RedirectToAction("Index", "Visita");
        }

        public ActionResult Realizar(long /* visitaId */ id)
        {
            if (id == 0)
            {
                AddMessageError("Visita não encontrada.");
                return RedirectToAction("Index", "Visita");
            }

            Visita visita = VisitaService.Instance.GetVisita(id);

            if (visita == null)
            {
                AddMessageError("Visita não encontrada.");
                return RedirectToAction("Index", "Visita");
            }

            if (CurrentUser.Id != visita.CuidadorId)
            {
                AddMessageError("Visita associada ao seu usuário.");
                return RedirectToAction("Index", "Visita");
            }

            visita.StatusVisita = StatusVisita.Realizada;

            VisitaService.Instance.Update(visita);

            AddMessageSuccess("Visita realizada.");

            return RedirectToAction("Detalhes", "Visita", new {id = id});
        }
    }
}