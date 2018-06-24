USE [devmygame]
GO
/****** Object:  Table [dbo].[EmprestimoJogo]    Script Date: 24/06/2018 05:38:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmprestimoJogo](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[CodigoUsuario] [int] NOT NULL,
	[CodigoJogo] [int] NOT NULL,
	[DataEmprestimo] [datetime] NOT NULL,
	[DataDevolucao] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Jogo]    Script Date: 24/06/2018 05:38:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Jogo](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](255) NOT NULL,
	[DataCadastro] [datetime] NOT NULL,
	[CodigoUsuarioDono] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 24/06/2018 05:38:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[Codigo] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](255) NOT NULL,
	[Email] [nvarchar](320) NOT NULL,
	[Senha] [nvarchar](32) NOT NULL,
	[Telefone] [nvarchar](15) NOT NULL,
	[DataCadastro] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[EmprestimoJogo] ON 
GO
INSERT [dbo].[EmprestimoJogo] ([Codigo], [CodigoUsuario], [CodigoJogo], [DataEmprestimo], [DataDevolucao]) VALUES (61, 3, 42, CAST(N'2018-06-24T02:59:01.477' AS DateTime), CAST(N'2018-06-24T03:12:40.967' AS DateTime))
GO
INSERT [dbo].[EmprestimoJogo] ([Codigo], [CodigoUsuario], [CodigoJogo], [DataEmprestimo], [DataDevolucao]) VALUES (62, 3, 42, CAST(N'2018-06-24T02:59:01.490' AS DateTime), CAST(N'2018-06-24T03:12:40.967' AS DateTime))
GO
INSERT [dbo].[EmprestimoJogo] ([Codigo], [CodigoUsuario], [CodigoJogo], [DataEmprestimo], [DataDevolucao]) VALUES (64, 3, 42, CAST(N'2018-06-24T02:59:01.647' AS DateTime), CAST(N'2018-06-24T03:12:40.967' AS DateTime))
GO
INSERT [dbo].[EmprestimoJogo] ([Codigo], [CodigoUsuario], [CodigoJogo], [DataEmprestimo], [DataDevolucao]) VALUES (65, 3, 42, CAST(N'2018-06-24T02:59:07.380' AS DateTime), CAST(N'2018-06-24T03:12:40.967' AS DateTime))
GO
INSERT [dbo].[EmprestimoJogo] ([Codigo], [CodigoUsuario], [CodigoJogo], [DataEmprestimo], [DataDevolucao]) VALUES (68, 3, 56, CAST(N'2018-06-24T03:11:44.607' AS DateTime), CAST(N'2018-06-24T03:11:48.747' AS DateTime))
GO
INSERT [dbo].[EmprestimoJogo] ([Codigo], [CodigoUsuario], [CodigoJogo], [DataEmprestimo], [DataDevolucao]) VALUES (70, 5, 60, CAST(N'2018-06-24T03:12:29.750' AS DateTime), NULL)
GO
INSERT [dbo].[EmprestimoJogo] ([Codigo], [CodigoUsuario], [CodigoJogo], [DataEmprestimo], [DataDevolucao]) VALUES (71, 3, 59, CAST(N'2018-06-24T03:12:33.690' AS DateTime), NULL)
GO
INSERT [dbo].[EmprestimoJogo] ([Codigo], [CodigoUsuario], [CodigoJogo], [DataEmprestimo], [DataDevolucao]) VALUES (72, 5, 42, CAST(N'2018-06-24T03:12:38.927' AS DateTime), CAST(N'2018-06-24T03:12:40.967' AS DateTime))
GO
INSERT [dbo].[EmprestimoJogo] ([Codigo], [CodigoUsuario], [CodigoJogo], [DataEmprestimo], [DataDevolucao]) VALUES (73, 5, 39, CAST(N'2018-06-24T03:12:48.280' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[EmprestimoJogo] OFF
GO
SET IDENTITY_INSERT [dbo].[Jogo] ON 
GO
INSERT [dbo].[Jogo] ([Codigo], [Nome], [DataCadastro], [CodigoUsuarioDono]) VALUES (33, N'Twisted Metal', CAST(N'2018-06-24T01:51:51.480' AS DateTime), 5)
GO
INSERT [dbo].[Jogo] ([Codigo], [Nome], [DataCadastro], [CodigoUsuarioDono]) VALUES (39, N'Yoshi Island', CAST(N'2018-06-24T02:40:05.723' AS DateTime), 4)
GO
INSERT [dbo].[Jogo] ([Codigo], [Nome], [DataCadastro], [CodigoUsuarioDono]) VALUES (41, N'Pokemon Y', CAST(N'2018-06-24T02:43:02.090' AS DateTime), 4)
GO
INSERT [dbo].[Jogo] ([Codigo], [Nome], [DataCadastro], [CodigoUsuarioDono]) VALUES (42, N'Pokemon X', CAST(N'2018-06-24T02:43:11.970' AS DateTime), 4)
GO
INSERT [dbo].[Jogo] ([Codigo], [Nome], [DataCadastro], [CodigoUsuarioDono]) VALUES (44, N'Super Mario World', CAST(N'2018-06-24T02:43:19.187' AS DateTime), 4)
GO
INSERT [dbo].[Jogo] ([Codigo], [Nome], [DataCadastro], [CodigoUsuarioDono]) VALUES (51, N'International Super Star Soccer Deluxe Edition', CAST(N'2018-06-24T02:44:50.963' AS DateTime), 4)
GO
INSERT [dbo].[Jogo] ([Codigo], [Nome], [DataCadastro], [CodigoUsuarioDono]) VALUES (56, N'Pokemon Ruby', CAST(N'2018-06-24T03:04:07.543' AS DateTime), 4)
GO
INSERT [dbo].[Jogo] ([Codigo], [Nome], [DataCadastro], [CodigoUsuarioDono]) VALUES (59, N'Pokemon Diamond', CAST(N'2018-06-24T03:12:18.333' AS DateTime), 4)
GO
INSERT [dbo].[Jogo] ([Codigo], [Nome], [DataCadastro], [CodigoUsuarioDono]) VALUES (60, N'Mario Kart', CAST(N'2018-06-24T03:12:23.727' AS DateTime), 4)
GO
INSERT [dbo].[Jogo] ([Codigo], [Nome], [DataCadastro], [CodigoUsuarioDono]) VALUES (62, N'Rockman X', CAST(N'2018-06-24T04:20:38.200' AS DateTime), 10)
GO
INSERT [dbo].[Jogo] ([Codigo], [Nome], [DataCadastro], [CodigoUsuarioDono]) VALUES (71, N'Donkey Kong', CAST(N'2018-06-24T05:05:01.133' AS DateTime), 3)
GO
INSERT [dbo].[Jogo] ([Codigo], [Nome], [DataCadastro], [CodigoUsuarioDono]) VALUES (72, N'Rockman', CAST(N'2018-06-24T05:05:16.503' AS DateTime), 3)
GO
INSERT [dbo].[Jogo] ([Codigo], [Nome], [DataCadastro], [CodigoUsuarioDono]) VALUES (73, N'Rockman', CAST(N'2018-06-24T05:05:52.430' AS DateTime), 4)
GO
SET IDENTITY_INSERT [dbo].[Jogo] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 
GO
INSERT [dbo].[Usuario] ([Codigo], [Nome], [Email], [Senha], [Telefone], [DataCadastro]) VALUES (3, N'Rafaela Ferreira', N'rafaela.jesus.ferreira@gmail.com', N'202CB962AC59075B964B07152D234B70', N'27999754974', CAST(N'2018-06-22T03:56:38.803' AS DateTime))
GO
INSERT [dbo].[Usuario] ([Codigo], [Nome], [Email], [Senha], [Telefone], [DataCadastro]) VALUES (4, N'Marcio Oliveira', N'deoliveira.marcioroberto@gmail.com', N'698DC19D489C4E4DB73E28A713EAB07B', N'27998155406', CAST(N'2018-06-22T22:51:47.173' AS DateTime))
GO
INSERT [dbo].[Usuario] ([Codigo], [Nome], [Email], [Senha], [Telefone], [DataCadastro]) VALUES (5, N'João da Silva', N'joaosilva@bol.com.br', N'827CCB0EEA8A706C4C34A16891F84E7B', N'21988556677', CAST(N'2018-06-24T01:47:07.190' AS DateTime))
GO
INSERT [dbo].[Usuario] ([Codigo], [Nome], [Email], [Senha], [Telefone], [DataCadastro]) VALUES (6, N'Alessandro Del Piero', N'delpiero@milan.it', N'698DC19D489C4E4DB73E28A713EAB07B', N'99999999999', CAST(N'2018-06-24T03:14:26.253' AS DateTime))
GO
INSERT [dbo].[Usuario] ([Codigo], [Nome], [Email], [Senha], [Telefone], [DataCadastro]) VALUES (7, N'Vasco Euzébio', N'vascoeuzebio@vasco.com.br', N'202CB962AC59075B964B07152D234B70', N'teste', CAST(N'2018-06-24T03:16:16.997' AS DateTime))
GO
INSERT [dbo].[Usuario] ([Codigo], [Nome], [Email], [Senha], [Telefone], [DataCadastro]) VALUES (8, N'Lucas Barreto', N'lucasfera@bol.com.br', N'202CB962AC59075B964B07152D234B70', N'99999999999', CAST(N'2018-06-24T03:19:43.167' AS DateTime))
GO
INSERT [dbo].[Usuario] ([Codigo], [Nome], [Email], [Senha], [Telefone], [DataCadastro]) VALUES (9, N'Marcio Silva', N'marciosilva@hotmail.com', N'698DC19D489C4E4DB73E28A713EAB07B', N'99999999999', CAST(N'2018-06-24T03:35:30.093' AS DateTime))
GO
INSERT [dbo].[Usuario] ([Codigo], [Nome], [Email], [Senha], [Telefone], [DataCadastro]) VALUES (10, N'Carlos Cunha', N'carlos@sp.gov.br', N'698DC19D489C4E4DB73E28A713EAB07B', N'99999999999', CAST(N'2018-06-24T04:12:34.073' AS DateTime))
GO
INSERT [dbo].[Usuario] ([Codigo], [Nome], [Email], [Senha], [Telefone], [DataCadastro]) VALUES (11, N'Paola Moreira', N'pm@r7.com', N'698DC19D489C4E4DB73E28A713EAB07B', N'28998987654', CAST(N'2018-06-24T05:13:02.483' AS DateTime))
GO
INSERT [dbo].[Usuario] ([Codigo], [Nome], [Email], [Senha], [Telefone], [DataCadastro]) VALUES (12, N'Sergio Reis', N'sergioreis@somlivre.com', N'698DC19D489C4E4DB73E28A713EAB07B', N'41988762345', CAST(N'2018-06-24T05:15:01.433' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
/****** Object:  Index [UNQ_EmprestimoJogo_01]    Script Date: 24/06/2018 05:38:15 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UNQ_EmprestimoJogo_01] ON [dbo].[EmprestimoJogo]
(
	[CodigoUsuario] ASC,
	[CodigoJogo] ASC,
	[DataEmprestimo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EmprestimoJogo]  WITH CHECK ADD FOREIGN KEY([CodigoUsuario])
REFERENCES [dbo].[Usuario] ([Codigo])
GO
ALTER TABLE [dbo].[EmprestimoJogo]  WITH CHECK ADD FOREIGN KEY([CodigoJogo])
REFERENCES [dbo].[Jogo] ([Codigo])
GO
ALTER TABLE [dbo].[Jogo]  WITH CHECK ADD FOREIGN KEY([CodigoUsuarioDono])
REFERENCES [dbo].[Usuario] ([Codigo])
GO
