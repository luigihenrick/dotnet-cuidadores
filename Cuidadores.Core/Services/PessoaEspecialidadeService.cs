using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using Cuidadores.Core.Entities;
using System.Collections.Generic;

namespace Cuidadores.Core.Services
{
    public class PessoaEspecialidadeService
    {

        private readonly string _connection = ConfigurationManager.ConnectionStrings["Cuidadores"].ConnectionString;

        private static PessoaEspecialidadeService _instance;

        public static PessoaEspecialidadeService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PessoaEspecialidadeService();
                }

                return _instance;
            }
        }

        public bool Create(long idPessoa, long? especialidade)
        {
            if (especialidade != 0)
            {
                string insertPessoa = @"insert into tbl_pessoa_especialidade ([Criado],[IdPessoa],[IdEspecialidade])
                                        VALUES (GETDATE(), @IdPessoa, @IdEspecialidade); ";

                using (var conn = new SqlConnection(_connection))
                {
                    conn.ExecuteScalar<long>(insertPessoa, new
                    {
                        IdPessoa = idPessoa,
                        IdEspecialidade = especialidade,
                    });
                }
                return true;
            }

            return false;
        }

        public int ExecuteNonQuery(string query, object parameters = null)
        {
            int result;

            using (var conn = new SqlConnection(_connection))
            {
                result = conn.Execute(query, parameters);
            }

            return result;
        }

        public IEnumerable<PessoaEspecialidade> GetPessoasEspecialidades(string query, object parameters = null)
        {
            IEnumerable<PessoaEspecialidade> pessoasEspecialidades;

            using (var conn = new SqlConnection(_connection))
            {
                pessoasEspecialidades = conn.Query<PessoaEspecialidade>(query, parameters);
            }

            return pessoasEspecialidades;
        }

        public IEnumerable<PessoaEspecialidade> All()
        {
            string selectAllQuery = "select * from tbl_pessoa_especialidade";

            IEnumerable<PessoaEspecialidade> pessoasEspecialidades;

            using (var conn = new SqlConnection(_connection))
            {
                pessoasEspecialidades = conn.Query<PessoaEspecialidade>(selectAllQuery);
            }

            return pessoasEspecialidades;
        }
    }
}
