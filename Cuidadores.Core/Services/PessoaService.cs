using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using System.Text;
using Cuidadores.Core.Entities;
using Cuidadores.Util.Extensions;

namespace Cuidadores.Core.Services
{
    public class PessoaService
    {
        private readonly string _connection = ConfigurationManager.ConnectionStrings["Cuidadores"].ConnectionString;

        private static PessoaService _instance;

        public static PessoaService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PessoaService();
                }

                return _instance;
            }
        }

        public Pessoa GetPessoa(long id)
        {
            List<Pessoa> pessoas;

            using (var conn = new SqlConnection(_connection))
            {
                pessoas = conn.Query<Pessoa>(
                    @"select * from tbl_pessoa Where Id = @Id", new { Id = id }).ToList();
            }

            if (!pessoas.Any())
            {
                return null;
            }

            return pessoas.FirstOrDefault();
        }

        public Pessoa GetPessoaByEmail(string email)
        {
            List<Pessoa> pessoas;

            using (var conn = new SqlConnection(_connection))
            {
                pessoas = conn.Query<Pessoa>(
                    @"select * from tbl_pessoa Where Email = @Email", new { Email = email }).ToList();
            }

            if (!pessoas.Any())
            {
                return null;
            }

            return pessoas.FirstOrDefault();
        }

        public IEnumerable<Pessoa> GetPessoaByName(string nome)
        {
            string term = "%" + nome.EncodeForLike() + "%";

            IEnumerable<Pessoa> pessoas;

            using (var conn = new SqlConnection(_connection))
            {
                pessoas = conn.Query<Pessoa>(
                    @"select * from tbl_pessoa Where Nome Like @term", new { term = term });
            }

            return pessoas;
        }

        public IEnumerable<Pessoa> GetPessoas(string query, object parameters = null)
        {
            IEnumerable<Pessoa> pessoas;

            using (var conn = new SqlConnection(_connection))
            {
                pessoas = conn.Query<Pessoa>(query, parameters);
            }

            return pessoas;
        }

        public Pessoa Create(Pessoa pessoa)
        {
            bool registrosGuardadosComSucesso;

            //VALUES (GETDATE(), @Nome, @Telefone, @Email, @Rg, @Cpf, PWDENCRYPT(@Senha));
            string insertPessoa = @"insert into tbl_pessoa ([Criado] ,[Nome] ,[Telefone] ,[Email] ,[Rg] ,[Cpf] ,[Senha]
                                    ,[Cep] ,[Endereco] ,[Numero] ,[Complemento] ,[Bairro] ,[Cidade] ,[Uf], [TipoPessoa])
                                        VALUES (GETDATE(), @Nome, @Telefone, @Email, @Rg, @Cpf, @Senha,
                                            @Cep, @Endereco, @Numero, @Complemento, @Bairro, @Cidade, @Uf, @TipoPessoa);
                                        select cast(SCOPE_IDENTITY() as bigint); ";

            long pessoaId = 0;

            using (var conn = new SqlConnection(_connection))
            {
                pessoaId = conn.ExecuteScalar<long>(insertPessoa, new
                {
                    Nome = pessoa.Nome,
                    Telefone = pessoa.Telefone,
                    Email = pessoa.Email,
                    Rg = pessoa.Rg.OnlyDigits(),
                    Cpf = pessoa.Cpf.OnlyDigits(),
                    Senha = pessoa.Senha.ToMD5(),
                    Cep = pessoa.Cep.OnlyDigits(),
                    Endereco = pessoa.Endereco,
                    Numero = pessoa.Numero,
                    Complemento = pessoa.Complemento,
                    Bairro = pessoa.Bairro,
                    Cidade = pessoa.Cidade,
                    Uf = pessoa.Uf.ToString(),
                    TipoPessoa = pessoa.TipoPessoa.ToString()
                });
            }

            if (pessoaId == 0)
            {
                return null;
            }

            return GetPessoa(pessoaId);
        }

        public Pessoa Update(Pessoa pessoa)
        {
            string updateQuery = @"update tbl_pessoa 
            SET 
                  [Atualizado] = GETDATE()
                  ,[Nome] = @Nome
                  ,[Telefone] = @Telefone
                  ,[Email] = @Email
                  ,[Rg] = @Rg
                  ,[Cpf] = @Cpf
                  ,[Senha] = @Senha
                  ,[Cep] = @Cep
                  ,[Endereco] = @Endereco
                  ,[Numero] = @Numero
                  ,[Complemento] = @Complemento
                  ,[Bairro] = @Bairro
                  ,[Cidade] = @Cidade
                  ,[Uf] = @Uf
                  ,[TipoPessoa] = @TipoPessoa
             WHERE Id = @Id";

            using (var conn = new SqlConnection(_connection))
            {
                conn.ExecuteScalar<int>(updateQuery, 
                    new
                    {
                        Id = pessoa.Id,

                        Nome = pessoa.Nome,
                        Telefone = pessoa.Telefone,
                        Email = pessoa.Email,
                        Rg = pessoa.Rg.OnlyDigits(),
                        Cpf = pessoa.Cpf.OnlyDigits(),
                        Senha = pessoa.Senha,

                        Cep = pessoa.Cep.OnlyDigits(),
                        Endereco = pessoa.Endereco,
                        Numero = pessoa.Numero,
                        Complemento = pessoa.Complemento,
                        Bairro = pessoa.Bairro,
                        Cidade = pessoa.Cidade,
                        Uf = pessoa.Uf.ToString(),
                        TipoPessoa = pessoa.TipoPessoa.ToString()
                    });
            }

            return GetPessoa(pessoa.Id);
        }

        public bool Delete(long pessoaId)
        {
            string deleteQuery = @"delete from tbl_pessoa Where Id = @Id";

            using (var conn = new SqlConnection(_connection))
            {
                try
                {
                    conn.Execute(deleteQuery, new { Id = pessoaId });
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return true;
        }

        public IEnumerable<Pessoa> All()
        {
            string selectAllQuery = "select * from tbl_pessoa";

            IEnumerable<Pessoa> pessoas;

            using (var conn = new SqlConnection(_connection))
            {
                pessoas = conn.Query<Pessoa>(selectAllQuery);
            }

            return pessoas;
        }

    }
}