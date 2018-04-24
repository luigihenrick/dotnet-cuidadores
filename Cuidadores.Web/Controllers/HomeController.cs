using System;
using System.Linq;
using System.Web.Mvc;
using Cuidadores.Core.Entities;
using Cuidadores.Core.Services;
using Cuidadores.Util.Extensions;
using Cuidadores.Web.Models.Pessoa;

namespace Cuidadores.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Pessoa model)
        {
            Pessoa pessoa = PessoaService.Instance.GetPessoaByEmail(model.Email);

            if (pessoa == null)
            {
                AddMessageWarning("E-mail não cadastrado");
                return RedirectToAction("Login", "Home");
            }

            if (pessoa.Senha != model.Senha.ToMD5())
            {
                AddMessageError("Senha incorreta");
                return RedirectToAction("Login", "Home");
            }

            Session["Usuario"] = pessoa;

            AddMessageSuccess("Login efetuado");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Session["Usuario"] = null;
            AddMessageSuccess("Logout realizado");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Cadastro(long id)
        {
            Pessoa pessoa;

            if (id == 0)
            {
                pessoa = new Pessoa();
            }
            else
            {
                if (Session["Usuario"] == null || ((Pessoa) Session["Usuario"]).Id != id)
                {
                    return RedirectToAction("Login", "Home");
                }
                
                pessoa = PessoaService.Instance.GetPessoa(id);
            }

            return View(new PessoaVm(pessoa));
        }

        [HttpPost]
        public ActionResult Cadastro(PessoaVm model)
        {
            if (model == null)
            {
                AddMessageError("Model NULL");
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid)
            {
                AddMessageError("Verifique os campos");
                return View(new PessoaVm(model));
            }

            Pessoa pessoa;

            if (model.Id == 0)
            {
                if (PessoaService.Instance.GetPessoaByEmail(model.Email) != null)
                {
                    AddMessageError("E-mail já cadastrado");
                    return RedirectToAction("Index", "Home");
                }

                pessoa = PessoaService.Instance.Create(model);
            }
            else
            {
                pessoa = PessoaService.Instance.GetPessoa(model.Id);

                pessoa.Nome = model.Nome;
                pessoa.Telefone = model.Telefone;
                pessoa.Email = model.Email;
                pessoa.Rg = model.Rg;
                pessoa.Cpf = model.Cpf;
                pessoa.Senha = !string.IsNullOrWhiteSpace(model.Senha) ? model.Senha.ToMD5() : pessoa.Senha;

                pessoa.Cep = model.Cep;
                pessoa.Endereco = model.Endereco;
                pessoa.Numero = model.Numero;
                pessoa.Complemento = model.Complemento;
                pessoa.Bairro = model.Bairro;
                pessoa.Cidade = model.Cidade;
                pessoa.Uf = model.Uf;

                pessoa.TipoPessoa = model.TipoPessoa;

                PessoaService.Instance.Update(pessoa);
            }

            // Adiciona especialidades
            PessoaEspecialidadeService.Instance.ExecuteNonQuery(
                @"delete from tbl_pessoa_especialidade
                        where IdPessoa = @IdPessoa ", new { IdPessoa = pessoa.Id });

            if (model.EspecialidadesSelecionadas != null && model.EspecialidadesSelecionadas.Any())
            {
                foreach (long especialidadeId in model.EspecialidadesSelecionadas)
                {
                    PessoaEspecialidadeService.Instance.Create(pessoa.Id, especialidadeId);
                }
            }

            AddMessageSuccess("Dados salvos com sucesso");
            return RedirectToAction("Index", "Home");
        }

        public JsonResult BuscaCep(string cep)
        {
            cep = cep.OnlyDigits();
            
            var ws = new WSCorreios.AtendeClienteClient();

            try
            {
                var resposta = ws.consultaCEP(cep);

                return Json(new
                {
                    success = true,
                    endereco = resposta.end,
                    complemento = resposta.complemento,
                    bairro = resposta.bairro,
                    cidade = resposta.cidade,
                    estado = resposta.uf
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new {success = false, error = "Falha ao buscar CEP: " + ex.Message}, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult QuemSomos()
        {
            return View();
        }

    }
}