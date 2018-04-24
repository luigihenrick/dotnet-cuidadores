-----------------------------------------------------------
-- Scrips iniciais

if object_id('tbl_pessoa', 'U') is not null 
	drop table tbl_pessoa;
go

create table tbl_pessoa 
(
	[Id] BIGINT NOT NULL PRIMARY KEY IDENTITY,
	[Criado] DATETIME NULL,
	[Atualizado] DATETIME NULL,
	[Nome] NVARCHAR(100) NULL,
	[Telefone] NVARCHAR(30) NULL,
	[Email] NVARCHAR(300) NULL,
	[Rg] NVARCHAR(20) NULL,
	[Cpf] NVARCHAR(11) NULL,
	[Senha] NVARCHAR(100) NULL,
	[ComprovanteEnderecoFilename] NVARCHAR(500) NULL,
	[ComprovanteEnderecoLenght] INT NULL,
	[ComprovanteEnderecoBytes] VARBINARY(MAX) NULL,
)

GO

alter table tbl_pessoa add Cep NVARCHAR(8) NULL;
alter table tbl_pessoa add Endereco NVARCHAR(100) NULL;
alter table tbl_pessoa add Numero NVARCHAR(40) NULL;
alter table tbl_pessoa add Complemento NVARCHAR(60) NULL;
alter table tbl_pessoa add Bairro NVARCHAR(60) NULL;
alter table tbl_pessoa add Cidade NVARCHAR(60) NULL;
alter table tbl_pessoa add Uf NCHAR(2) NULL;
alter table tbl_pessoa add TipoPessoa NVARCHAR(50) NULL;

go

insert into tbl_pessoa ([Criado] ,[Nome] ,[Telefone] ,[Email] ,[Rg] ,[Cpf] ,[Senha], [TipoPessoa], [Cep], [Endereco], [Numero], [Bairro], [Cidade], [Uf])
     values (GETDATE(), 'Luigi Henrick','(11) 9 1234-1234', 'luigihenrick@gmail.com', '123123123', '12312312312', '202CB962AC59075B964B07152D234B70', 'Cuidador', '01101010', 'Rua Um', '001', 'Bairro Um', 'São Paulo', 'SP')

insert into tbl_pessoa ([Criado] ,[Nome] ,[Telefone] ,[Email] ,[Rg] ,[Cpf] ,[Senha], [TipoPessoa], [Cep], [Endereco], [Numero], [Bairro], [Cidade], [Uf])
     values (GETDATE(), 'Pedro Bartoli de Oliveira','(11) 9 1234-1234', 'pedroo.sp@hotmail.com', '123123123', '12312312312', '202CB962AC59075B964B07152D234B70', 'Paciente', '01101010', 'Rua Dois', '002', 'Bairro Dois', 'São Paulo', 'SP')

insert into tbl_pessoa ([Criado] ,[Nome] ,[Telefone] ,[Email] ,[Rg] ,[Cpf] ,[Senha], [TipoPessoa], [Cep], [Endereco], [Numero], [Bairro], [Cidade], [Uf])
     values (GETDATE(), 'Samir El Hassan','(11) 9 1234-1234', 'samir-hassan@live.com', '123123123', '12312312312', '202CB962AC59075B964B07152D234B70', 'Cuidador', '01101010', 'Rua Três', '003', 'Bairro Três', 'São Paulo', 'SP')

insert into tbl_pessoa ([Criado] ,[Nome] ,[Telefone] ,[Email] ,[Rg] ,[Cpf] ,[Senha], [TipoPessoa], [Cep], [Endereco], [Numero], [Bairro], [Cidade], [Uf])
     values (GETDATE(), 'Paulo Vitor Jardim','(11) 9 1234-1234', 'paulo.victor@hotmail.com', '123123123', '12312312312', '202CB962AC59075B964B07152D234B70', 'Paciente', '01101010', 'Rua Quatro', '004', 'Bairro Quatro', 'São Paulo', 'SP')

insert into tbl_pessoa ([Criado] ,[Nome] ,[Telefone] ,[Email] ,[Rg] ,[Cpf] ,[Senha], [TipoPessoa], [Cep], [Endereco], [Numero], [Bairro], [Cidade], [Uf])
     values (GETDATE(), 'Marcelino de Paula','(11) 9 1234-1234', 'marcelino.paula@hotmail.com', '123123123', '12312312312', '202CB962AC59075B964B07152D234B70', 'Cuidador', '01101010', 'Rua Cinco', '005', 'Bairro Cinco', 'São Paulo', 'SP')

insert into tbl_pessoa ([Criado] ,[Nome] ,[Telefone] ,[Email] ,[Rg] ,[Cpf] ,[Senha], [TipoPessoa], [Cep], [Endereco], [Numero], [Bairro], [Cidade], [Uf])
     values (GETDATE(), 'Thiago Augusto Braga Ruiz','(11) 9 1234-1234', 'thiago.augusto@hotmail.com', '123123123', '12312312312', '202CB962AC59075B964B07152D234B70', 'Paciente', '01101010', 'Rua Seis', '006', 'Bairro Seis', 'São Paulo', 'SP')


GO