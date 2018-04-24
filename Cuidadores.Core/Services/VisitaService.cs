using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Cuidadores.Core.Entities;
using Cuidadores.Core.Models;
using Cuidadores.Util.Extensions;

namespace Cuidadores.Core.Services
{
    public class VisitaService
    {
        private readonly string _connection = ConfigurationManager.ConnectionStrings["Cuidadores"].ConnectionString;

        private static VisitaService _instance;

        public static VisitaService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new VisitaService();
                }

                return _instance;
            }
        }

        public Visita GetVisita(long id)
        {
            List<Visita> visitas;

            using (var conn = new SqlConnection(_connection))
            {
                visitas = conn.Query<Visita>(
                    @"select * from tbl_visita Where Id = @Id", new { Id = id }).ToList();
            }

            if (!visitas.Any())
            {
                return null;
            }

            return visitas.FirstOrDefault();
        }

        public Visita Create(Visita visita)
        {
            string insertPessoa = @"insert into tbl_visita ([Criado] ,[PacienteId] ,[CuidadorId] ,[DataVisita] ,[StatusVisita])
                                        VALUES (GETDATE(), @PacienteId, @CuidadorId, @DataVisita, @StatusVisita);
                                        select cast(SCOPE_IDENTITY() as bigint); ";

            long visitaId;

            using (var conn = new SqlConnection(_connection))
            {
                visitaId = conn.ExecuteScalar<long>(insertPessoa, new
                {
                    PacienteId = visita.PacienteId,
                    CuidadorId = visita.CuidadorId,
                    DataVisita = visita.DataVisita,
                    Justificativa = visita.Justificativa,
                    StatusVisita = StatusVisita.Pendente.GetDisplayName(),
                });
            }

            if (visitaId == 0)
            {
                return null;
            }

            return GetVisita(visitaId);
        }

        public Visita Update(Visita visita)
        {
            string updateQuery = @"update tbl_visita 
            SET 
                  [Atualizado] = GETDATE(),
                  [CuidadorId] = @CuidadorId,
                  [PacienteId] = @PacienteId,
                  [DataVisita] = @DataVisita,
                  [Justificativa] = @Justificativa,
                  [StatusVisita] = @StatusVisita
             WHERE Id = @Id";

            using (var conn = new SqlConnection(_connection))
            {
                conn.ExecuteScalar<int>(updateQuery, new
                {
                    Id = visita.Id,
                    CuidadorId = visita.CuidadorId,
                    PacienteId = visita.PacienteId,
                    DataVisita = visita.DataVisita,
                    Justificativa = visita.Justificativa,
                    StatusVisita = visita.StatusVisita.GetDisplayName(),
                });
            }

            return GetVisita(visita.Id);
        }

        public IEnumerable<Visita> Find(string query, object parameters = null)
        {
            IEnumerable<Visita> visitas;

            using (var conn = new SqlConnection(_connection))
            {
                visitas = conn.Query<Visita>(query, parameters);
            }

            return visitas;
        }

        public VisitaVm FindVisitaVm(long id)
        {
            VisitaVm visita;

            using (var conn = new SqlConnection(_connection))
            {
                visita = conn.QueryFirst<VisitaVm>(@"select * from v_visita where Id = @Id", new {Id = id});
            }

            return visita;
        }

        public IEnumerable<VisitaVm> FindVisitasVm(string query, object parameters = null)
        {
            IEnumerable<VisitaVm> visitas;

            using (var conn = new SqlConnection(_connection))
            {
                visitas = conn.Query<VisitaVm>(query, parameters);
            }

            return visitas;
        }

        public IEnumerable<Visita> All()
        {
            string selectAllQuery = "select * from tbl_visita";

            IEnumerable<Visita> visitas;

            using (var conn = new SqlConnection(_connection))
            {
                visitas = conn.Query<Visita>(selectAllQuery);
            }

            return visitas;
        }
    }
}