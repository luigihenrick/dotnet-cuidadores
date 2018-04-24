using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web.Mvc;
using Cuidadores.Core.Entities;
using Cuidadores.Core.Services;
using Cuidadores.Util.Extensions;
using Cuidadores.Web.Models.Pessoa;

namespace Cuidadores.Web.Controllers
{
    public class PessoaController : BaseController
    {
        public ActionResult Detalhes(long id)
        {
            if (id == 0)
            {
                AddMessageError("Pessoa não encontrada.");
                return RedirectToAction("Index", "Home");
            }

            Pessoa pessoa = PessoaService.Instance.GetPessoa(id);

            if (pessoa == null)
            {
                AddMessageError("Pessoa não encontrada.");
                return RedirectToAction("Index", "Home");
            }

            var model = new DetalhesVm(pessoa, CurrentUser);

            return View(model);
        }

        [HttpGet]
        public ActionResult Buscar()
        {
            return View(new PessoaBuscarVm());
        }

        [HttpPost]
        public ActionResult Buscar(string nome, string bairro, string cidade, UF? uf, long? especialidade)
        {
            var parameters = string.Empty;
            dynamic parametersObj = new ExpandoObject();
            
            if (!string.IsNullOrWhiteSpace(nome))
            {
                nome = "%" + nome.EncodeForLike() + "%";
                parameters = string.Format("{0} and Nome Like @Nome", parameters);
                parametersObj.Nome = nome;
            }
            if (!string.IsNullOrWhiteSpace(bairro))
            {
                bairro = "%" + bairro.EncodeForLike() + "%";
                parameters = string.Format("{0} and Bairro Like @Bairro", parameters);
                parametersObj.Bairro = bairro;
            }
            if (!string.IsNullOrWhiteSpace(cidade))
            {
                cidade = "%" + cidade.EncodeForLike() + "%";
                parameters = string.Format("{0} and Cidade Like @Cidade", parameters);
                parametersObj.Cidade = cidade;
            }

            string query = string.Format(@"select * from tbl_pessoa
                                Where 1 = 1
                                    and Uf = @Uf
                                    and TipoPessoa = 'Cuidador'
                                    {0}", parameters);

            List<Pessoa> pessoas = PessoaService.Instance.GetPessoas(
                query,
                new
                {
                    Nome = !string.IsNullOrWhiteSpace(nome) ? parametersObj.Nome : "", 
                    Bairro = !string.IsNullOrWhiteSpace(bairro) ? parametersObj.Bairro : "", 
                    Cidade = !string.IsNullOrWhiteSpace(cidade) ? parametersObj.Cidade : "", 
                    Uf = uf.ToString()
                }).ToList();

            if (especialidade != null && especialidade != 0)
            {
                List<Pessoa> pessoasFiltrada = new List<Pessoa>();
                var registrosEspecialidade = PessoaEspecialidadeService.Instance.GetPessoasEspecialidades(
                    @"select * from tbl_pessoa_especialidade 
                    Where 1 = 1 
                    and IdEspecialidade = @IdEspecialidade",
                new { IdEspecialidade = especialidade }).ToList();

                foreach (var pessoa in pessoas)
                    if (registrosEspecialidade.Any(x => x.IdPessoa == pessoa.Id)) pessoasFiltrada.Add(pessoa);

                pessoas = pessoasFiltrada;
            }

            return View(new PessoaBuscarVm(pessoas));
        }
    }
}