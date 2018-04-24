using System.Collections.Generic;
using Cuidadores.Core.Services;

namespace Cuidadores.Web.Models.Pessoa
{
    public class DetalhesVm: Core.Entities.Pessoa
    {
        public Core.Entities.Pessoa Paciente { get; set; }
        public IEnumerable<Core.Entities.Visita> Visitas { get; set; }

        public DetalhesVm(Core.Entities.Pessoa pessoa, Core.Entities.Pessoa currentUser)
        {
            Id = pessoa.Id;
            Criado = pessoa.Criado;
            Atualizado = pessoa.Atualizado;
            Nome = pessoa.Nome;
            Telefone = pessoa.Telefone;
            Email = pessoa.Email;
            Rg = pessoa.Rg;
            Cpf = pessoa.Cpf;

            Cep = pessoa.Cep;
            Endereco = pessoa.Endereco;
            Numero = pessoa.Numero;
            Complemento = pessoa.Complemento;
            Bairro = pessoa.Bairro;
            Cidade = pessoa.Cidade;
            Uf = pessoa.Uf;

            TipoPessoa = pessoa.TipoPessoa;

            if (currentUser.Id == 0)
            {
                return;
            }

            if (pessoa.TipoPessoa == Core.Entities.TipoPessoa.Cuidador
                && currentUser.TipoPessoa == Core.Entities.TipoPessoa.Paciente)
            {
                Paciente = currentUser;

                Visitas = VisitaService.Instance.Find(
                    @"select * from tbl_visita 
                    where 1 = 1 
                        and CuidadorId = @CuidadorId 
                        and PacienteId = @PacienteId ",
                    new {CuidadorId = Id, PacienteId = Paciente.Id});
            }
            else
            {
                Visitas = new List<Core.Entities.Visita>();
            }
        }
    }
}