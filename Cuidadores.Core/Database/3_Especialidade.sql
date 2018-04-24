-----------------------------------------------------------
-- Scrips para especialidade


if object_id('tbl_especialidade', 'U') is not null 
	drop table tbl_especialidade;
go

CREATE TABLE tbl_especialidade(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY,
	[Criado] DATETIME NULL,
	[Atualizado] DATETIME NULL,
	[Descricao] NVARCHAR(100) NOT NULL,
)

GO

insert into tbl_especialidade ([Criado] ,[Descricao])
     values (GETDATE(), 'Idosos')

insert into tbl_especialidade ([Criado] ,[Descricao])
     values (GETDATE(), 'Adultos')

insert into tbl_especialidade ([Criado] ,[Descricao])
     values (GETDATE(), 'Crianças')
GO

if object_id('tbl_pessoa_especialidade', 'U') is not null 
	drop table tbl_pessoa_especialidade;
go

CREATE TABLE tbl_pessoa_especialidade(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY,
	[Criado] DATETIME NULL,
	[Atualizado] DATETIME NULL,
	[IdEspecialidade] BIGINT NOT NULL,
	[IdPessoa] BIGINT NULL
)

GO

insert into tbl_pessoa_especialidade ([Criado] ,[IdEspecialidade] ,[IdPessoa])
     values (GETDATE(), 1, 1)
	 
insert into tbl_pessoa_especialidade ([Criado] ,[IdEspecialidade] ,[IdPessoa])
     values (GETDATE(), 3, 1)

insert into tbl_pessoa_especialidade ([Criado] ,[IdEspecialidade] ,[IdPessoa])
     values (GETDATE(), 1, 2)
	 
insert into tbl_pessoa_especialidade ([Criado] ,[IdEspecialidade] ,[IdPessoa])
     values (GETDATE(), 2, 2)

insert into tbl_pessoa_especialidade ([Criado] ,[IdEspecialidade] ,[IdPessoa])
     values (GETDATE(), 2, 3)
	 
insert into tbl_pessoa_especialidade ([Criado] ,[IdEspecialidade] ,[IdPessoa])
     values (GETDATE(), 3, 3)

insert into tbl_pessoa_especialidade ([Criado] ,[IdEspecialidade] ,[IdPessoa])
     values (GETDATE(), 1, 4)
	 
insert into tbl_pessoa_especialidade ([Criado] ,[IdEspecialidade] ,[IdPessoa])
     values (GETDATE(), 3, 4)

GO