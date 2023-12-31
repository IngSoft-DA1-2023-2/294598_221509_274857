USE [master]
GO
/****** Object:  Database [FinTracDb]    Script Date: 16/11/2023 20:41:31 ******/
CREATE DATABASE [FinTracDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FinTracDb', FILENAME = N'C:\Users\betin\FinTracDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FinTracDb_log', FILENAME = N'C:\Users\betin\FinTracDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [FinTracDb] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FinTracDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FinTracDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FinTracDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FinTracDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FinTracDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FinTracDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [FinTracDb] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [FinTracDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FinTracDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FinTracDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FinTracDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FinTracDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FinTracDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FinTracDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FinTracDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FinTracDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [FinTracDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FinTracDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FinTracDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FinTracDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FinTracDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FinTracDb] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [FinTracDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FinTracDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FinTracDb] SET  MULTI_USER 
GO
ALTER DATABASE [FinTracDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FinTracDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FinTracDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FinTracDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FinTracDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FinTracDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [FinTracDb] SET QUERY_STORE = OFF
GO
USE [FinTracDb]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 16/11/2023 20:41:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cambio]    Script Date: 16/11/2023 20:41:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cambio](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EspacioId] [int] NOT NULL,
	[FechaDeCambio] [datetime2](7) NOT NULL,
	[Moneda] [int] NOT NULL,
	[Pesos] [float] NOT NULL,
 CONSTRAINT [PK_Cambio] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categoria]    Script Date: 16/11/2023 20:41:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categoria](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EspacioId] [int] NOT NULL,
	[EstadoActivo] [bit] NOT NULL,
	[Tipo] [int] NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
	[FechaCreacion] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Categoria] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cuenta]    Script Date: 16/11/2023 20:41:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cuenta](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EspacioId] [int] NOT NULL,
	[Saldo] [float] NOT NULL,
	[Moneda] [int] NOT NULL,
	[FechaCreacion] [datetime2](7) NOT NULL,
	[Tipo_cuenta] [nvarchar](max) NOT NULL,
	[Nombre] [nvarchar](max) NULL,
	[FechaCierre] [datetime2](7) NULL,
	[BancoEmisor] [nvarchar](max) NULL,
	[NumeroTarjeta] [nvarchar](max) NULL,
 CONSTRAINT [PK_Cuenta] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Espacios]    Script Date: 16/11/2023 20:41:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Espacios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AdminId] [int] NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Espacios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EspacioUsuario]    Script Date: 16/11/2023 20:41:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EspacioUsuario](
	[EspaciosId] [int] NOT NULL,
	[UsuariosInvitadosId] [int] NOT NULL,
 CONSTRAINT [PK_EspacioUsuario] PRIMARY KEY CLUSTERED 
(
	[EspaciosId] ASC,
	[UsuariosInvitadosId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Objetivo]    Script Date: 16/11/2023 20:41:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Objetivo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EspacioId] [int] NOT NULL,
	[Titulo] [nvarchar](max) NOT NULL,
	[MontoMaximo] [float] NOT NULL,
	[Token] [nvarchar](max) NULL,
 CONSTRAINT [PK_Objetivo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ObjetivoCategoria]    Script Date: 16/11/2023 20:41:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ObjetivoCategoria](
	[CategoriasId] [int] NOT NULL,
	[ObjetivosId] [int] NOT NULL,
 CONSTRAINT [PK_ObjetivoCategoria] PRIMARY KEY CLUSTERED 
(
	[CategoriasId] ASC,
	[ObjetivosId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transaccion]    Script Date: 16/11/2023 20:41:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaccion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoriaId] [int] NOT NULL,
	[CuentaId] [int] NOT NULL,
	[EspacioId] [int] NOT NULL,
	[Moneda] [int] NOT NULL,
	[Titulo] [nvarchar](max) NOT NULL,
	[FechaTransaccion] [datetime2](7) NOT NULL,
	[Monto] [float] NOT NULL,
	[Tipo_Transaccion] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Transaccion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 16/11/2023 20:41:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdEspacioPrincipal] [int] NOT NULL,
	[Direccion] [nvarchar](max) NOT NULL,
	[Contrasena] [nvarchar](max) NOT NULL,
	[Correo] [nvarchar](max) NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
	[Apellido] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231116081027_InitialEspacioDataBase', N'7.0.13')
GO
SET IDENTITY_INSERT [dbo].[Cambio] ON 

INSERT [dbo].[Cambio] ([Id], [EspacioId], [FechaDeCambio], [Moneda], [Pesos]) VALUES (1, 1, CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2), 1, 30)
INSERT [dbo].[Cambio] ([Id], [EspacioId], [FechaDeCambio], [Moneda], [Pesos]) VALUES (2, 1, CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2), 2, 60)
INSERT [dbo].[Cambio] ([Id], [EspacioId], [FechaDeCambio], [Moneda], [Pesos]) VALUES (3, 1, CAST(N'2023-11-20T00:00:00.0000000' AS DateTime2), 1, 70)
INSERT [dbo].[Cambio] ([Id], [EspacioId], [FechaDeCambio], [Moneda], [Pesos]) VALUES (4, 1, CAST(N'2023-11-21T00:00:00.0000000' AS DateTime2), 1, 43)
INSERT [dbo].[Cambio] ([Id], [EspacioId], [FechaDeCambio], [Moneda], [Pesos]) VALUES (5, 1, CAST(N'2023-11-29T00:00:00.0000000' AS DateTime2), 2, 50)
INSERT [dbo].[Cambio] ([Id], [EspacioId], [FechaDeCambio], [Moneda], [Pesos]) VALUES (6, 1, CAST(N'2023-11-22T00:00:00.0000000' AS DateTime2), 2, 70)
SET IDENTITY_INSERT [dbo].[Cambio] OFF
GO
SET IDENTITY_INSERT [dbo].[Categoria] ON 

INSERT [dbo].[Categoria] ([Id], [EspacioId], [EstadoActivo], [Tipo], [Nombre], [FechaCreacion]) VALUES (1, 1, 1, 1, N'Sueldo', CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Categoria] ([Id], [EspacioId], [EstadoActivo], [Tipo], [Nombre], [FechaCreacion]) VALUES (2, 1, 1, 1, N'inversiones', CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Categoria] ([Id], [EspacioId], [EstadoActivo], [Tipo], [Nombre], [FechaCreacion]) VALUES (3, 1, 1, 1, N'Cheques', CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Categoria] ([Id], [EspacioId], [EstadoActivo], [Tipo], [Nombre], [FechaCreacion]) VALUES (4, 1, 1, 1, N'aguinaldo', CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Categoria] ([Id], [EspacioId], [EstadoActivo], [Tipo], [Nombre], [FechaCreacion]) VALUES (5, 1, 1, 0, N'supermercado', CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Categoria] ([Id], [EspacioId], [EstadoActivo], [Tipo], [Nombre], [FechaCreacion]) VALUES (6, 1, 1, 1, N'electrodomesticos', CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Categoria] ([Id], [EspacioId], [EstadoActivo], [Tipo], [Nombre], [FechaCreacion]) VALUES (7, 1, 1, 1, N'viajes', CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Categoria] ([Id], [EspacioId], [EstadoActivo], [Tipo], [Nombre], [FechaCreacion]) VALUES (8, 1, 0, 0, N'seguro ', CAST(N'2023-11-16T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Categoria] ([Id], [EspacioId], [EstadoActivo], [Tipo], [Nombre], [FechaCreacion]) VALUES (9, 1, 1, 0, N'auto', CAST(N'2023-10-30T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Categoria] ([Id], [EspacioId], [EstadoActivo], [Tipo], [Nombre], [FechaCreacion]) VALUES (10, 1, 1, 0, N'Fiestas', CAST(N'2023-10-30T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Categoria] ([Id], [EspacioId], [EstadoActivo], [Tipo], [Nombre], [FechaCreacion]) VALUES (11, 1, 1, 0, N'Boletos', CAST(N'2023-10-30T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Categoria] ([Id], [EspacioId], [EstadoActivo], [Tipo], [Nombre], [FechaCreacion]) VALUES (13, 1, 1, 0, N'Restaurant', CAST(N'2023-10-30T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Categoria] OFF
GO
SET IDENTITY_INSERT [dbo].[Cuenta] ON 

INSERT [dbo].[Cuenta] ([Id], [EspacioId], [Saldo], [Moneda], [FechaCreacion], [Tipo_cuenta], [Nombre], [FechaCierre], [BancoEmisor], [NumeroTarjeta]) VALUES (1, 1, 699400, 0, CAST(N'2023-11-16T20:13:19.5640682' AS DateTime2), N'Ahorro', N'Santander', NULL, NULL, NULL)
INSERT [dbo].[Cuenta] ([Id], [EspacioId], [Saldo], [Moneda], [FechaCreacion], [Tipo_cuenta], [Nombre], [FechaCierre], [BancoEmisor], [NumeroTarjeta]) VALUES (2, 1, 78293, 1, CAST(N'2023-11-16T20:13:30.9531110' AS DateTime2), N'Ahorro', N'Banco republica', NULL, NULL, NULL)
INSERT [dbo].[Cuenta] ([Id], [EspacioId], [Saldo], [Moneda], [FechaCreacion], [Tipo_cuenta], [Nombre], [FechaCierre], [BancoEmisor], [NumeroTarjeta]) VALUES (3, 1, -7080, 2, CAST(N'2023-11-16T20:13:50.6592565' AS DateTime2), N'Ahorro', N'Banco nacion', NULL, NULL, NULL)
INSERT [dbo].[Cuenta] ([Id], [EspacioId], [Saldo], [Moneda], [FechaCreacion], [Tipo_cuenta], [Nombre], [FechaCierre], [BancoEmisor], [NumeroTarjeta]) VALUES (4, 1, 7000000, 0, CAST(N'2023-11-16T20:14:05.6043097' AS DateTime2), N'Credito', NULL, CAST(N'2024-01-18T00:00:00.0000000' AS DateTime2), N'Mastercard', N'6754')
INSERT [dbo].[Cuenta] ([Id], [EspacioId], [Saldo], [Moneda], [FechaCreacion], [Tipo_cuenta], [Nombre], [FechaCierre], [BancoEmisor], [NumeroTarjeta]) VALUES (5, 1, 9982, 1, CAST(N'2023-11-16T20:14:23.7864328' AS DateTime2), N'Credito', NULL, CAST(N'2024-01-26T00:00:00.0000000' AS DateTime2), N'Visa dorada', N'6589')
INSERT [dbo].[Cuenta] ([Id], [EspacioId], [Saldo], [Moneda], [FechaCreacion], [Tipo_cuenta], [Nombre], [FechaCierre], [BancoEmisor], [NumeroTarjeta]) VALUES (6, 1, 89700, 2, CAST(N'2023-11-16T20:14:46.8512403' AS DateTime2), N'Credito', NULL, CAST(N'2024-03-08T00:00:00.0000000' AS DateTime2), N'Tarjeta etiqueta negra', N'4567')
SET IDENTITY_INSERT [dbo].[Cuenta] OFF
GO
SET IDENTITY_INSERT [dbo].[Espacios] ON 

INSERT [dbo].[Espacios] ([Id], [AdminId], [Nombre]) VALUES (1, 1, N'Principal Betina')
INSERT [dbo].[Espacios] ([Id], [AdminId], [Nombre]) VALUES (2, 2, N'Principal maxi')
INSERT [dbo].[Espacios] ([Id], [AdminId], [Nombre]) VALUES (3, 3, N'Principal diego')
INSERT [dbo].[Espacios] ([Id], [AdminId], [Nombre]) VALUES (4, 4, N'Principal mateo')
INSERT [dbo].[Espacios] ([Id], [AdminId], [Nombre]) VALUES (5, 1, N'Ort university')
SET IDENTITY_INSERT [dbo].[Espacios] OFF
GO
INSERT [dbo].[EspacioUsuario] ([EspaciosId], [UsuariosInvitadosId]) VALUES (2, 1)
INSERT [dbo].[EspacioUsuario] ([EspaciosId], [UsuariosInvitadosId]) VALUES (3, 1)
INSERT [dbo].[EspacioUsuario] ([EspaciosId], [UsuariosInvitadosId]) VALUES (4, 1)
INSERT [dbo].[EspacioUsuario] ([EspaciosId], [UsuariosInvitadosId]) VALUES (1, 2)
INSERT [dbo].[EspacioUsuario] ([EspaciosId], [UsuariosInvitadosId]) VALUES (3, 2)
INSERT [dbo].[EspacioUsuario] ([EspaciosId], [UsuariosInvitadosId]) VALUES (4, 2)
INSERT [dbo].[EspacioUsuario] ([EspaciosId], [UsuariosInvitadosId]) VALUES (4, 3)
INSERT [dbo].[EspacioUsuario] ([EspaciosId], [UsuariosInvitadosId]) VALUES (1, 4)
GO
SET IDENTITY_INSERT [dbo].[Objetivo] ON 

INSERT [dbo].[Objetivo] ([Id], [EspacioId], [Titulo], [MontoMaximo], [Token]) VALUES (1, 1, N'Menos comida chatarra', 666, NULL)
INSERT [dbo].[Objetivo] ([Id], [EspacioId], [Titulo], [MontoMaximo], [Token]) VALUES (2, 1, N'Menos restaurant', 7890, NULL)
INSERT [dbo].[Objetivo] ([Id], [EspacioId], [Titulo], [MontoMaximo], [Token]) VALUES (3, 1, N'Menos bebidas', 8000, NULL)
INSERT [dbo].[Objetivo] ([Id], [EspacioId], [Titulo], [MontoMaximo], [Token]) VALUES (4, 1, N'Menos fiesta y alcohol', 9000, NULL)
INSERT [dbo].[Objetivo] ([Id], [EspacioId], [Titulo], [MontoMaximo], [Token]) VALUES (5, 1, N'Menos nafta', 8000, NULL)
INSERT [dbo].[Objetivo] ([Id], [EspacioId], [Titulo], [MontoMaximo], [Token]) VALUES (6, 1, N'Menos viajes', 90000, NULL)
INSERT [dbo].[Objetivo] ([Id], [EspacioId], [Titulo], [MontoMaximo], [Token]) VALUES (7, 1, N'Menos fiesta y restaurant', 9000, NULL)
INSERT [dbo].[Objetivo] ([Id], [EspacioId], [Titulo], [MontoMaximo], [Token]) VALUES (8, 1, N'Ahorro total', 7890, NULL)
INSERT [dbo].[Objetivo] ([Id], [EspacioId], [Titulo], [MontoMaximo], [Token]) VALUES (9, 1, N'Menos boletos', 80, NULL)
INSERT [dbo].[Objetivo] ([Id], [EspacioId], [Titulo], [MontoMaximo], [Token]) VALUES (10, 1, N'Menos automc', 800, NULL)
SET IDENTITY_INSERT [dbo].[Objetivo] OFF
GO
INSERT [dbo].[ObjetivoCategoria] ([CategoriasId], [ObjetivosId]) VALUES (5, 1)
INSERT [dbo].[ObjetivoCategoria] ([CategoriasId], [ObjetivosId]) VALUES (13, 2)
INSERT [dbo].[ObjetivoCategoria] ([CategoriasId], [ObjetivosId]) VALUES (5, 3)
INSERT [dbo].[ObjetivoCategoria] ([CategoriasId], [ObjetivosId]) VALUES (5, 4)
INSERT [dbo].[ObjetivoCategoria] ([CategoriasId], [ObjetivosId]) VALUES (10, 4)
INSERT [dbo].[ObjetivoCategoria] ([CategoriasId], [ObjetivosId]) VALUES (8, 5)
INSERT [dbo].[ObjetivoCategoria] ([CategoriasId], [ObjetivosId]) VALUES (9, 5)
INSERT [dbo].[ObjetivoCategoria] ([CategoriasId], [ObjetivosId]) VALUES (8, 6)
INSERT [dbo].[ObjetivoCategoria] ([CategoriasId], [ObjetivosId]) VALUES (9, 6)
INSERT [dbo].[ObjetivoCategoria] ([CategoriasId], [ObjetivosId]) VALUES (10, 7)
INSERT [dbo].[ObjetivoCategoria] ([CategoriasId], [ObjetivosId]) VALUES (13, 7)
INSERT [dbo].[ObjetivoCategoria] ([CategoriasId], [ObjetivosId]) VALUES (5, 8)
INSERT [dbo].[ObjetivoCategoria] ([CategoriasId], [ObjetivosId]) VALUES (8, 8)
INSERT [dbo].[ObjetivoCategoria] ([CategoriasId], [ObjetivosId]) VALUES (9, 8)
INSERT [dbo].[ObjetivoCategoria] ([CategoriasId], [ObjetivosId]) VALUES (10, 8)
INSERT [dbo].[ObjetivoCategoria] ([CategoriasId], [ObjetivosId]) VALUES (11, 8)
INSERT [dbo].[ObjetivoCategoria] ([CategoriasId], [ObjetivosId]) VALUES (13, 8)
INSERT [dbo].[ObjetivoCategoria] ([CategoriasId], [ObjetivosId]) VALUES (11, 9)
INSERT [dbo].[ObjetivoCategoria] ([CategoriasId], [ObjetivosId]) VALUES (8, 10)
INSERT [dbo].[ObjetivoCategoria] ([CategoriasId], [ObjetivosId]) VALUES (10, 10)
GO
SET IDENTITY_INSERT [dbo].[Transaccion] ON 

INSERT [dbo].[Transaccion] ([Id], [CategoriaId], [CuentaId], [EspacioId], [Moneda], [Titulo], [FechaTransaccion], [Monto], [Tipo_Transaccion]) VALUES (1, 5, 1, 1, 0, N'Comida', CAST(N'2023-11-16T20:15:04.4864701' AS DateTime2), 600, N'TransaccionCosto')
INSERT [dbo].[Transaccion] ([Id], [CategoriaId], [CuentaId], [EspacioId], [Moneda], [Titulo], [FechaTransaccion], [Monto], [Tipo_Transaccion]) VALUES (2, 5, 2, 1, 1, N'Pizza', CAST(N'2023-11-16T20:15:18.1394923' AS DateTime2), 7, N'TransaccionCosto')
INSERT [dbo].[Transaccion] ([Id], [CategoriaId], [CuentaId], [EspacioId], [Moneda], [Titulo], [FechaTransaccion], [Monto], [Tipo_Transaccion]) VALUES (3, 5, 6, 1, 2, N'Chocolates', CAST(N'2023-11-16T20:15:38.9178372' AS DateTime2), 90, N'TransaccionCosto')
INSERT [dbo].[Transaccion] ([Id], [CategoriaId], [CuentaId], [EspacioId], [Moneda], [Titulo], [FechaTransaccion], [Monto], [Tipo_Transaccion]) VALUES (4, 10, 2, 1, 1, N'Vino', CAST(N'2023-11-16T20:16:50.6815832' AS DateTime2), 700, N'TransaccionCosto')
INSERT [dbo].[Transaccion] ([Id], [CategoriaId], [CuentaId], [EspacioId], [Moneda], [Titulo], [FechaTransaccion], [Monto], [Tipo_Transaccion]) VALUES (5, 11, 5, 1, 1, N'Viajes rocha', CAST(N'2023-11-16T20:18:01.5265804' AS DateTime2), 6699, N'TransaccionCosto')
INSERT [dbo].[Transaccion] ([Id], [CategoriaId], [CuentaId], [EspacioId], [Moneda], [Titulo], [FechaTransaccion], [Monto], [Tipo_Transaccion]) VALUES (6, 9, 5, 1, 1, N'Nafta', CAST(N'2023-11-16T20:19:38.6363403' AS DateTime2), 899, N'TransaccionCosto')
INSERT [dbo].[Transaccion] ([Id], [CategoriaId], [CuentaId], [EspacioId], [Moneda], [Titulo], [FechaTransaccion], [Monto], [Tipo_Transaccion]) VALUES (7, 1, 2, 1, 1, N'Sueldo', CAST(N'2023-11-16T20:20:43.4041978' AS DateTime2), 70000, N'TransaccionIngreso')
INSERT [dbo].[Transaccion] ([Id], [CategoriaId], [CuentaId], [EspacioId], [Moneda], [Titulo], [FechaTransaccion], [Monto], [Tipo_Transaccion]) VALUES (8, 1, 5, 1, 1, N'Inversion oro', CAST(N'2023-11-16T20:21:57.2561139' AS DateTime2), 790, N'TransaccionIngreso')
INSERT [dbo].[Transaccion] ([Id], [CategoriaId], [CuentaId], [EspacioId], [Moneda], [Titulo], [FechaTransaccion], [Monto], [Tipo_Transaccion]) VALUES (9, 2, 6, 1, 2, N'inversion plata', CAST(N'2023-11-16T20:23:33.5827373' AS DateTime2), 790, N'TransaccionIngreso')
INSERT [dbo].[Transaccion] ([Id], [CategoriaId], [CuentaId], [EspacioId], [Moneda], [Titulo], [FechaTransaccion], [Monto], [Tipo_Transaccion]) VALUES (10, 3, 5, 1, 1, N'Loteria', CAST(N'2023-11-16T20:25:16.6176220' AS DateTime2), 8900, N'TransaccionIngreso')
INSERT [dbo].[Transaccion] ([Id], [CategoriaId], [CuentaId], [EspacioId], [Moneda], [Titulo], [FechaTransaccion], [Monto], [Tipo_Transaccion]) VALUES (11, 11, 3, 1, 2, N'Loteria', CAST(N'2023-11-16T20:27:03.8688795' AS DateTime2), 7890, N'TransaccionCosto')
INSERT [dbo].[Transaccion] ([Id], [CategoriaId], [CuentaId], [EspacioId], [Moneda], [Titulo], [FechaTransaccion], [Monto], [Tipo_Transaccion]) VALUES (12, 11, 3, 1, 2, N'vacaciones peru', CAST(N'2023-11-16T20:29:38.8667569' AS DateTime2), 7890, N'TransaccionCosto')
SET IDENTITY_INSERT [dbo].[Transaccion] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([Id], [IdEspacioPrincipal], [Direccion], [Contrasena], [Correo], [Nombre], [Apellido]) VALUES (1, 1, N'av italia entre dublin y corcega', N'HOLAhola123', N'betina@gmail.com', N'Betina', N'Kadessian')
INSERT [dbo].[Usuarios] ([Id], [IdEspacioPrincipal], [Direccion], [Contrasena], [Correo], [Nombre], [Apellido]) VALUES (2, 2, N'av españa 3456', N'HOLAhola123', N'maxi@gmail.com', N'maxi', N'gimenez')
INSERT [dbo].[Usuarios] ([Id], [IdEspacioPrincipal], [Direccion], [Contrasena], [Correo], [Nombre], [Apellido]) VALUES (3, 3, N'ort 5604', N'HOLAhola123', N'diego@balbi.com', N'diego', N'balbi')
INSERT [dbo].[Usuarios] ([Id], [IdEspacioPrincipal], [Direccion], [Contrasena], [Correo], [Nombre], [Apellido]) VALUES (4, 4, N'av libertador 5042', N'HOLAhola123', N'mateo@gmail.com', N'mateo', N'arias')
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
/****** Object:  Index [IX_Cambio_EspacioId]    Script Date: 16/11/2023 20:41:31 ******/
CREATE NONCLUSTERED INDEX [IX_Cambio_EspacioId] ON [dbo].[Cambio]
(
	[EspacioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Categoria_EspacioId]    Script Date: 16/11/2023 20:41:31 ******/
CREATE NONCLUSTERED INDEX [IX_Categoria_EspacioId] ON [dbo].[Categoria]
(
	[EspacioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Cuenta_EspacioId]    Script Date: 16/11/2023 20:41:31 ******/
CREATE NONCLUSTERED INDEX [IX_Cuenta_EspacioId] ON [dbo].[Cuenta]
(
	[EspacioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Espacios_AdminId]    Script Date: 16/11/2023 20:41:31 ******/
CREATE NONCLUSTERED INDEX [IX_Espacios_AdminId] ON [dbo].[Espacios]
(
	[AdminId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_EspacioUsuario_UsuariosInvitadosId]    Script Date: 16/11/2023 20:41:31 ******/
CREATE NONCLUSTERED INDEX [IX_EspacioUsuario_UsuariosInvitadosId] ON [dbo].[EspacioUsuario]
(
	[UsuariosInvitadosId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Objetivo_EspacioId]    Script Date: 16/11/2023 20:41:31 ******/
CREATE NONCLUSTERED INDEX [IX_Objetivo_EspacioId] ON [dbo].[Objetivo]
(
	[EspacioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ObjetivoCategoria_ObjetivosId]    Script Date: 16/11/2023 20:41:31 ******/
CREATE NONCLUSTERED INDEX [IX_ObjetivoCategoria_ObjetivosId] ON [dbo].[ObjetivoCategoria]
(
	[ObjetivosId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Transaccion_CategoriaId]    Script Date: 16/11/2023 20:41:31 ******/
CREATE NONCLUSTERED INDEX [IX_Transaccion_CategoriaId] ON [dbo].[Transaccion]
(
	[CategoriaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Transaccion_CuentaId]    Script Date: 16/11/2023 20:41:31 ******/
CREATE NONCLUSTERED INDEX [IX_Transaccion_CuentaId] ON [dbo].[Transaccion]
(
	[CuentaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Transaccion_EspacioId]    Script Date: 16/11/2023 20:41:31 ******/
CREATE NONCLUSTERED INDEX [IX_Transaccion_EspacioId] ON [dbo].[Transaccion]
(
	[EspacioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cambio]  WITH CHECK ADD  CONSTRAINT [FK_Cambio_Espacios_EspacioId] FOREIGN KEY([EspacioId])
REFERENCES [dbo].[Espacios] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cambio] CHECK CONSTRAINT [FK_Cambio_Espacios_EspacioId]
GO
ALTER TABLE [dbo].[Categoria]  WITH CHECK ADD  CONSTRAINT [FK_Categoria_Espacios_EspacioId] FOREIGN KEY([EspacioId])
REFERENCES [dbo].[Espacios] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Categoria] CHECK CONSTRAINT [FK_Categoria_Espacios_EspacioId]
GO
ALTER TABLE [dbo].[Cuenta]  WITH CHECK ADD  CONSTRAINT [FK_Cuenta_Espacios_EspacioId] FOREIGN KEY([EspacioId])
REFERENCES [dbo].[Espacios] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cuenta] CHECK CONSTRAINT [FK_Cuenta_Espacios_EspacioId]
GO
ALTER TABLE [dbo].[Espacios]  WITH CHECK ADD  CONSTRAINT [FK_Espacios_Usuarios_AdminId] FOREIGN KEY([AdminId])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Espacios] CHECK CONSTRAINT [FK_Espacios_Usuarios_AdminId]
GO
ALTER TABLE [dbo].[EspacioUsuario]  WITH CHECK ADD  CONSTRAINT [FK_EspacioUsuario_Espacios_EspaciosId] FOREIGN KEY([EspaciosId])
REFERENCES [dbo].[Espacios] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EspacioUsuario] CHECK CONSTRAINT [FK_EspacioUsuario_Espacios_EspaciosId]
GO
ALTER TABLE [dbo].[EspacioUsuario]  WITH CHECK ADD  CONSTRAINT [FK_EspacioUsuario_Usuarios_UsuariosInvitadosId] FOREIGN KEY([UsuariosInvitadosId])
REFERENCES [dbo].[Usuarios] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EspacioUsuario] CHECK CONSTRAINT [FK_EspacioUsuario_Usuarios_UsuariosInvitadosId]
GO
ALTER TABLE [dbo].[Objetivo]  WITH CHECK ADD  CONSTRAINT [FK_Objetivo_Espacios_EspacioId] FOREIGN KEY([EspacioId])
REFERENCES [dbo].[Espacios] ([Id])
GO
ALTER TABLE [dbo].[Objetivo] CHECK CONSTRAINT [FK_Objetivo_Espacios_EspacioId]
GO
ALTER TABLE [dbo].[ObjetivoCategoria]  WITH CHECK ADD  CONSTRAINT [FK_ObjetivoCategoria_Categoria_CategoriasId] FOREIGN KEY([CategoriasId])
REFERENCES [dbo].[Categoria] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ObjetivoCategoria] CHECK CONSTRAINT [FK_ObjetivoCategoria_Categoria_CategoriasId]
GO
ALTER TABLE [dbo].[ObjetivoCategoria]  WITH CHECK ADD  CONSTRAINT [FK_ObjetivoCategoria_Objetivo_ObjetivosId] FOREIGN KEY([ObjetivosId])
REFERENCES [dbo].[Objetivo] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ObjetivoCategoria] CHECK CONSTRAINT [FK_ObjetivoCategoria_Objetivo_ObjetivosId]
GO
ALTER TABLE [dbo].[Transaccion]  WITH CHECK ADD  CONSTRAINT [FK_Transaccion_Categoria_CategoriaId] FOREIGN KEY([CategoriaId])
REFERENCES [dbo].[Categoria] ([Id])
GO
ALTER TABLE [dbo].[Transaccion] CHECK CONSTRAINT [FK_Transaccion_Categoria_CategoriaId]
GO
ALTER TABLE [dbo].[Transaccion]  WITH CHECK ADD  CONSTRAINT [FK_Transaccion_Cuenta_CuentaId] FOREIGN KEY([CuentaId])
REFERENCES [dbo].[Cuenta] ([Id])
GO
ALTER TABLE [dbo].[Transaccion] CHECK CONSTRAINT [FK_Transaccion_Cuenta_CuentaId]
GO
ALTER TABLE [dbo].[Transaccion]  WITH CHECK ADD  CONSTRAINT [FK_Transaccion_Espacios_EspacioId] FOREIGN KEY([EspacioId])
REFERENCES [dbo].[Espacios] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Transaccion] CHECK CONSTRAINT [FK_Transaccion_Espacios_EspacioId]
GO
USE [master]
GO
ALTER DATABASE [FinTracDb] SET  READ_WRITE 
GO
