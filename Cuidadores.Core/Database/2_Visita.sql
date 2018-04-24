-----------------------------------------------------------
-- Scrips para visita

if object_id('tbl_visita', 'U') is not null 
	drop table tbl_visita;
go


create table tbl_visita 
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY,
	[Criado] DATETIME NULL,
	[Atualizado] DATETIME NULL,
	[MedicoId] BIGINT NULL,
	[PacienteId] BIGINT NULL,
	[DataVisita] DATETIME NULL
)
go

alter table tbl_visita drop column MedicoId;
alter table tbl_visita add CuidadorId BIGINT NULL;
alter table tbl_visita add StatusVisita NVARCHAR(50) NULL;
alter table tbl_visita add Justificativa NVARCHAR(1000) NULL;

go

if object_id('v_visita', 'V') is not null 
	drop view v_visita;
go

create view v_visita as 

select tv.*, 
	tc.Nome as CuidadorNome, tc.Email as CuidadorEmail, tc.Telefone as CuidadorTelefone, cast((tc.Endereco) + ', ' + (tc.Numero) + ' - ' + (tc.Bairro) + ', ' + (tc.Cidade) + ' - ' + (tc.Uf) as nvarchar(500)) as CuidadorEndereco, 
	tp.Nome as PacienteNome, tp.Email as PacienteEmail, tp.Telefone as PacienteTelefone, cast((tp.Endereco) + ', ' + (tp.Numero) + ' - ' + (tp.Bairro) + ', ' + (tp.Cidade) + ' - ' + (tp.Uf) as nvarchar(500)) as PacienteEndereco
from tbl_visita tv
join tbl_pessoa tc on tv.CuidadorId = tc.Id
join tbl_pessoa tp on tv.PacienteId = tp.Id

go
