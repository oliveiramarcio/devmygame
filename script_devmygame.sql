CREATE TABLE [dbo].[Usuario](
	[Codigo] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Nome] [nvarchar](255) NOT NULL,
	[Email] [nvarchar](320) NOT NULL,
	[Senha] [nvarchar](32) NOT NULL,
	[Telefone] [nvarchar](15) NOT NULL,
	[DataCadastro] [datetime] NOT NULL);

CREATE TABLE [dbo].[Jogo](
    [Codigo] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Nome] [nvarchar](255) NOT NULL,
	[ArquivoImagem] [nvarchar](255) NOT NULL,
	[DataCadastro] [datetime] NOT NULL);

CREATE TABLE [dbo].[EmprestimoJogo](
    [Codigo] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[CodigoUsuario] [int] NOT NULL FOREIGN KEY REFERENCES [dbo].[Usuario] (Codigo),
	[CodigoJogo] [int] NOT NULL FOREIGN KEY REFERENCES [dbo].[Jogo] (Codigo),
	[DataEmprestimo] [datetime] NOT NULL,
	[DataDevolucao] [datetime]);

CREATE UNIQUE INDEX UNQ_EmprestimoJogo_01 ON [dbo].[EmprestimoJogo] (CodigoUsuario, CodigoJogo, DataEmprestimo);
