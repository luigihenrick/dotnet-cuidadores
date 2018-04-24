using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Cuidadores.Core.Entities;

namespace Cuidadores.Core.Services
{
    public class EspecialidadeService
    {
        private readonly string _connection = ConfigurationManager.ConnectionStrings["Cuidadores"].ConnectionString;

        private static EspecialidadeService _instance;

        public static EspecialidadeService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EspecialidadeService();
                }

                return _instance;
            }
        }

        public IEnumerable<Especialidade> All()
        {
            string selectAllQuery = "select * from tbl_especialidade";

            IEnumerable<Especialidade> especialidades;

            using (var conn = new SqlConnection(_connection))
            {
                especialidades = conn.Query<Especialidade>(selectAllQuery);
            }

            return especialidades;
        }

        public Especialidade Create(Especialidade especialidade)
        {
            bool registrosGuardadosComSucesso;

            string insertEspecialidade = @"insert into tbl_especialidade ([Criado] ,[Atualizado] ,[Descricao])
                                        VALUES (GETDATE(), GETDATE(), @Descricao);
                                        select cast(SCOPE_IDENTITY() as bigint); ";

            long especialidadeId = 0;

            using (var conn = new SqlConnection(_connection))
            {
                especialidadeId = conn.ExecuteScalar<long>(insertEspecialidade, new
                {
                    Descricao = especialidade.Descricao
                });
            }

            if (especialidadeId == 0)
            {
                return null;
            }

            return GetEspecialidade(especialidadeId);
        }

        public Especialidade GetEspecialidade(long id)
        {
            List<Especialidade> especialidades;

            using (var conn = new SqlConnection(_connection))
            {
                especialidades = conn.Query<Especialidade>(
                    @"select * from tbl_especialidade Where Id = @Id", new { Id = id }).ToList();
            }

            if (!especialidades.Any())
            {
                return null;
            }

            return especialidades.FirstOrDefault();
        }

        public Especialidade Update(Especialidade especialidade)
        {
            string updateQuery = @"update tbl_especialidade 
            SET 
                  [Atualizado] = GETDATE(),
                  [Descricao] = @Descricao
             WHERE Id = @Id";

            using (var conn = new SqlConnection(_connection))
            {
                conn.ExecuteScalar<int>(updateQuery, new {Descricao = especialidade.Descricao, Id = especialidade.Id});
            }

            return GetEspecialidade(especialidade.Id);
        }

        public bool Delete(long especialidadeId)
        {
            string deleteQuery = @"delete from tbl_especialidade Where Id = @Id";

            using (var conn = new SqlConnection(_connection))
            {
                try
                {
                    conn.Execute(deleteQuery, new { Id = especialidadeId });
                }
                catch (Exception e)
                {
                    return false;
                }
            }

            return true;
        }
    }
}