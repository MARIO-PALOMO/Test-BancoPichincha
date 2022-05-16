/*
 Navicat Premium Data Transfer

 Source Server         : SQL Server Local
 Source Server Type    : SQL Server
 Source Server Version : 15002000
 Source Host           : DESKTOP-JJ7MJN7\SQLEXPRESS:1433
 Source Catalog        : Banco
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 15002000
 File Encoding         : 65001

 Date: 16/05/2022 12:43:01
*/


-- ----------------------------
-- Table structure for __bancoMigrationsHistory
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[__bancoMigrationsHistory]') AND type IN ('U'))
	DROP TABLE [dbo].[__bancoMigrationsHistory]
GO

CREATE TABLE [dbo].[__bancoMigrationsHistory] (
  [MigrationId] nvarchar(150) COLLATE Modern_Spanish_CI_AS  NOT NULL,
  [ProductVersion] nvarchar(32) COLLATE Modern_Spanish_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[__bancoMigrationsHistory] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of __bancoMigrationsHistory
-- ----------------------------
INSERT INTO [dbo].[__bancoMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220515231610_CreacionTablaPersona', N'5.0.17')
GO

INSERT INTO [dbo].[__bancoMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220515231714_CreacionTablaPersonaCampos', N'5.0.17')
GO

INSERT INTO [dbo].[__bancoMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220515232731_CreacionTablaClienteCampos', N'5.0.17')
GO

INSERT INTO [dbo].[__bancoMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220515233657_CreacionTablaMovimientoyCorreccionTipoDatoTablaCuenta', N'5.0.17')
GO

INSERT INTO [dbo].[__bancoMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220515234257_CreacionRelacionesCuentayMovimiento', N'5.0.17')
GO

INSERT INTO [dbo].[__bancoMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220516161906_ActualizarTipoDatoMovimiento', N'5.0.17')
GO


-- ----------------------------
-- Table structure for Cliente
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Cliente]') AND type IN ('U'))
	DROP TABLE [dbo].[Cliente]
GO

CREATE TABLE [dbo].[Cliente] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [Contraseña] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL,
  [Estado] bit  NOT NULL,
  [PersonaId] int  NOT NULL
)
GO

ALTER TABLE [dbo].[Cliente] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Cliente
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Cliente] ON
GO

INSERT INTO [dbo].[Cliente] ([Id], [Contraseña], [Estado], [PersonaId]) VALUES (N'1006', N'1234', N'1', N'1006')
GO

INSERT INTO [dbo].[Cliente] ([Id], [Contraseña], [Estado], [PersonaId]) VALUES (N'1007', N'5678', N'1', N'1007')
GO

INSERT INTO [dbo].[Cliente] ([Id], [Contraseña], [Estado], [PersonaId]) VALUES (N'1008', N'1245', N'1', N'1008')
GO

SET IDENTITY_INSERT [dbo].[Cliente] OFF
GO


-- ----------------------------
-- Table structure for Cuenta
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Cuenta]') AND type IN ('U'))
	DROP TABLE [dbo].[Cuenta]
GO

CREATE TABLE [dbo].[Cuenta] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [Numero] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL,
  [Tipo] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL,
  [SaldoInicial] decimal(18,2)  NOT NULL,
  [Estado] bit  NOT NULL,
  [ClienteId] int DEFAULT ((0)) NOT NULL
)
GO

ALTER TABLE [dbo].[Cuenta] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Cuenta
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Cuenta] ON
GO

INSERT INTO [dbo].[Cuenta] ([Id], [Numero], [Tipo], [SaldoInicial], [Estado], [ClienteId]) VALUES (N'4', N'478758', N'Ahorros', N'2000.00', N'1', N'1006')
GO

INSERT INTO [dbo].[Cuenta] ([Id], [Numero], [Tipo], [SaldoInicial], [Estado], [ClienteId]) VALUES (N'5', N'225487', N'Corriente', N'100.00', N'1', N'1007')
GO

INSERT INTO [dbo].[Cuenta] ([Id], [Numero], [Tipo], [SaldoInicial], [Estado], [ClienteId]) VALUES (N'6', N'495878', N'Ahorros', N'.00', N'1', N'1008')
GO

INSERT INTO [dbo].[Cuenta] ([Id], [Numero], [Tipo], [SaldoInicial], [Estado], [ClienteId]) VALUES (N'8', N'496825', N'Ahorros', N'540.00', N'1', N'1007')
GO

INSERT INTO [dbo].[Cuenta] ([Id], [Numero], [Tipo], [SaldoInicial], [Estado], [ClienteId]) VALUES (N'9', N'585545', N'Corriente', N'1000.00', N'1', N'1006')
GO

INSERT INTO [dbo].[Cuenta] ([Id], [Numero], [Tipo], [SaldoInicial], [Estado], [ClienteId]) VALUES (N'16', N'478758', NULL, N'.00', N'0', N'1006')
GO

INSERT INTO [dbo].[Cuenta] ([Id], [Numero], [Tipo], [SaldoInicial], [Estado], [ClienteId]) VALUES (N'17', N'225487', NULL, N'.00', N'0', N'1007')
GO

INSERT INTO [dbo].[Cuenta] ([Id], [Numero], [Tipo], [SaldoInicial], [Estado], [ClienteId]) VALUES (N'18', N'495878', NULL, N'.00', N'0', N'1008')
GO

SET IDENTITY_INSERT [dbo].[Cuenta] OFF
GO


-- ----------------------------
-- Table structure for Movimiento
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Movimiento]') AND type IN ('U'))
	DROP TABLE [dbo].[Movimiento]
GO

CREATE TABLE [dbo].[Movimiento] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [Fecha] datetime  NOT NULL,
  [Tipo] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL,
  [Valor] decimal(18,2)  NOT NULL,
  [Saldo] decimal(18,2)  NOT NULL,
  [CuentaId] int DEFAULT ((0)) NOT NULL
)
GO

ALTER TABLE [dbo].[Movimiento] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Movimiento
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Movimiento] ON
GO

INSERT INTO [dbo].[Movimiento] ([Id], [Fecha], [Tipo], [Valor], [Saldo], [CuentaId]) VALUES (N'3', N'2022-05-16 12:24:29.217', N'Retiro', N'575.00', N'1425.00', N'16')
GO

INSERT INTO [dbo].[Movimiento] ([Id], [Fecha], [Tipo], [Valor], [Saldo], [CuentaId]) VALUES (N'4', N'2022-05-16 12:26:04.257', N'Deposito', N'600.00', N'700.00', N'17')
GO

INSERT INTO [dbo].[Movimiento] ([Id], [Fecha], [Tipo], [Valor], [Saldo], [CuentaId]) VALUES (N'5', N'2022-05-16 12:26:44.293', N'Deposito', N'150.00', N'150.00', N'18')
GO

INSERT INTO [dbo].[Movimiento] ([Id], [Fecha], [Tipo], [Valor], [Saldo], [CuentaId]) VALUES (N'7', N'2022-05-16 12:27:47.000', N'Retiro', N'540.00', N'.00', N'8')
GO

SET IDENTITY_INSERT [dbo].[Movimiento] OFF
GO


-- ----------------------------
-- Table structure for Persona
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[Persona]') AND type IN ('U'))
	DROP TABLE [dbo].[Persona]
GO

CREATE TABLE [dbo].[Persona] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [Identificacion] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL,
  [Nombre] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL,
  [Genero] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL,
  [Edad] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL,
  [Direccion] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL,
  [Telefono] nvarchar(max) COLLATE Modern_Spanish_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[Persona] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of Persona
-- ----------------------------
SET IDENTITY_INSERT [dbo].[Persona] ON
GO

INSERT INTO [dbo].[Persona] ([Id], [Identificacion], [Nombre], [Genero], [Edad], [Direccion], [Telefono]) VALUES (N'1006', N'1745896523', N'Jose Lema', N'M', N'30', N'Otavalo sn y principal', N'098254785')
GO

INSERT INTO [dbo].[Persona] ([Id], [Identificacion], [Nombre], [Genero], [Edad], [Direccion], [Telefono]) VALUES (N'1007', N'1744745859', N'Marianela Montalvo', N'F', N'29', N'Amazonas y NNUU', N'097548965')
GO

INSERT INTO [dbo].[Persona] ([Id], [Identificacion], [Nombre], [Genero], [Edad], [Direccion], [Telefono]) VALUES (N'1008', N'1744745859', N'Juan Osorio', N'M', N'36', N'13 junio y Equinoccial', N'098874587')
GO

SET IDENTITY_INSERT [dbo].[Persona] OFF
GO


-- ----------------------------
-- Primary Key structure for table __bancoMigrationsHistory
-- ----------------------------
ALTER TABLE [dbo].[__bancoMigrationsHistory] ADD CONSTRAINT [PK___bancoMigrationsHistory] PRIMARY KEY CLUSTERED ([MigrationId])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for Cliente
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Cliente]', RESEED, 1008)
GO


-- ----------------------------
-- Indexes structure for table Cliente
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_Cliente_PersonaId]
ON [dbo].[Cliente] (
  [PersonaId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table Cliente
-- ----------------------------
ALTER TABLE [dbo].[Cliente] ADD CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for Cuenta
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Cuenta]', RESEED, 18)
GO


-- ----------------------------
-- Indexes structure for table Cuenta
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_Cuenta_ClienteId]
ON [dbo].[Cuenta] (
  [ClienteId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table Cuenta
-- ----------------------------
ALTER TABLE [dbo].[Cuenta] ADD CONSTRAINT [PK_Cuenta] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for Movimiento
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Movimiento]', RESEED, 7)
GO


-- ----------------------------
-- Indexes structure for table Movimiento
-- ----------------------------
CREATE NONCLUSTERED INDEX [IX_Movimiento_CuentaId]
ON [dbo].[Movimiento] (
  [CuentaId] ASC
)
GO


-- ----------------------------
-- Primary Key structure for table Movimiento
-- ----------------------------
ALTER TABLE [dbo].[Movimiento] ADD CONSTRAINT [PK_Movimiento] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for Persona
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[Persona]', RESEED, 1008)
GO


-- ----------------------------
-- Primary Key structure for table Persona
-- ----------------------------
ALTER TABLE [dbo].[Persona] ADD CONSTRAINT [PK_Persona] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Foreign Keys structure for table Cliente
-- ----------------------------
ALTER TABLE [dbo].[Cliente] ADD CONSTRAINT [FK_Cliente_Persona_PersonaId] FOREIGN KEY ([PersonaId]) REFERENCES [dbo].[Persona] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table Cuenta
-- ----------------------------
ALTER TABLE [dbo].[Cuenta] ADD CONSTRAINT [FK_Cuenta_Cliente_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [dbo].[Cliente] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO


-- ----------------------------
-- Foreign Keys structure for table Movimiento
-- ----------------------------
ALTER TABLE [dbo].[Movimiento] ADD CONSTRAINT [FK_Movimiento_Cuenta_CuentaId] FOREIGN KEY ([CuentaId]) REFERENCES [dbo].[Cuenta] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

