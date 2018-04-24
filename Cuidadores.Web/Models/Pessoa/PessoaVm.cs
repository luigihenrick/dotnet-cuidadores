using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cuidadores.Core.Entities;
using Cuidadores.Core.Services;

namespace Cuidadores.Web.Models.Pessoa
{
    public class PessoaVm: Core.Entities.Pessoa
    {
        public List<long> EspecialidadesSelecionadas { get; set; }
        public List<SelectListItem> EspecialidadesList { get; set; }

        public PessoaVm()
        {
            EspecialidadesList = new List<SelectListItem>();

            IEnumerable<Especialidade> especialidades = EspecialidadeService.Instance.All();

            EspecialidadesList.AddRange(especialidades.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Descricao
            }));
        }

        public PessoaVm(Core.Entities.Pessoa pessoa)
        {
            EspecialidadesList = new List<SelectListItem>();

            IEnumerable<Especialidade> especialidades = EspecialidadeService.Instance.All();

            IEnumerable<PessoaEspecialidade> especialidadesPessoa =
                PessoaEspecialidadeService.Instance.GetPessoasEspecialidades(
                    @"select * from tbl_pessoa_especialidade 
                    Where IdPessoa = @IdPessoa", new {IdPessoa = pessoa.Id});

            EspecialidadesList.AddRange(especialidades.Select(x => new SelectListItem()
            {
                Selected = (especialidadesPessoa != null && especialidadesPessoa.Any(y => y.IdEspecialidade == x.Id)),
                Value = x.Id.ToString(),
                Text = x.Descricao
            }));

            Nome = pessoa.Nome;
            Telefone = pessoa.Telefone;
            Email = pessoa.Email;
            Rg = pessoa.Rg;
            Cpf = pessoa.Cpf;
            Senha = pessoa.Senha;

            Cep = pessoa.Cep;
            Endereco = pessoa.Endereco;
            Numero = pessoa.Numero;
            Complemento = pessoa.Complemento;
            Bairro = pessoa.Bairro;
            Cidade = pessoa.Cidade;
            Uf = pessoa.Uf;

            TipoPessoa = pessoa.TipoPessoa;

            ComprovanteEnderecoFilename = pessoa.ComprovanteEnderecoFilename;
            ComprovanteEnderecoLenght = pessoa.ComprovanteEnderecoLenght;
            ComprovanteEnderecoBytes = pessoa.ComprovanteEnderecoBytes;
        }
    }
}