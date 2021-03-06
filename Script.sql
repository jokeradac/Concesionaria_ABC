USE [master]
GO
/****** Object:  Database [Agencia]    Script Date: 20/02/2019 04:43:34 p.m. ******/
CREATE DATABASE [Agencia]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Agencia', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Agencia.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Agencia_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Agencia_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Agencia] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Agencia].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Agencia] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Agencia] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Agencia] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Agencia] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Agencia] SET ARITHABORT OFF 
GO
ALTER DATABASE [Agencia] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Agencia] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Agencia] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Agencia] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Agencia] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Agencia] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Agencia] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Agencia] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Agencia] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Agencia] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Agencia] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Agencia] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Agencia] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Agencia] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Agencia] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Agencia] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Agencia] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Agencia] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Agencia] SET  MULTI_USER 
GO
ALTER DATABASE [Agencia] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Agencia] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Agencia] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Agencia] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Agencia] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Agencia]
GO
/****** Object:  Table [dbo].[Automovil]    Script Date: 20/02/2019 04:43:34 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Automovil](
	[Folio] [int] IDENTITY(1,1) NOT NULL,
	[Marca] [varchar](25) NOT NULL,
	[Modelo] [varchar](45) NOT NULL,
	[Color] [varchar](15) NOT NULL,
	[TipoTransmision] [varchar](15) NOT NULL,
	[Estetica] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Auto] PRIMARY KEY CLUSTERED 
(
	[Folio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Bitacora]    Script Date: 20/02/2019 04:43:34 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bitacora](
	[idBitacora] [int] IDENTITY(1,1) NOT NULL,
	[Solicitud] [int] NOT NULL,
	[Automovil] [int] NOT NULL,
 CONSTRAINT [PK_Bitacora] PRIMARY KEY CLUSTERED 
(
	[idBitacora] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Solicitud]    Script Date: 20/02/2019 04:43:34 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Solicitud](
	[idSolicitud] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [date] NOT NULL,
	[Lote] [int] NOT NULL,
 CONSTRAINT [PK_Solicitud] PRIMARY KEY CLUSTERED 
(
	[idSolicitud] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Bitacora]  WITH CHECK ADD  CONSTRAINT [FK_Bitacora_Automovil1] FOREIGN KEY([Automovil])
REFERENCES [dbo].[Automovil] ([Folio])
GO
ALTER TABLE [dbo].[Bitacora] CHECK CONSTRAINT [FK_Bitacora_Automovil1]
GO
ALTER TABLE [dbo].[Bitacora]  WITH CHECK ADD  CONSTRAINT [FK_Bitacora_Solicitud] FOREIGN KEY([Solicitud])
REFERENCES [dbo].[Solicitud] ([idSolicitud])
GO
ALTER TABLE [dbo].[Bitacora] CHECK CONSTRAINT [FK_Bitacora_Solicitud]
GO
/****** Object:  StoredProcedure [dbo].[ActualizaCoche]    Script Date: 20/02/2019 04:43:34 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizaCoche]
@folio int,
@marca varchar(25) null,
@modelo varchar(25) null,
@color varchar(15) null,
@tipotransmision varchar(15) null,
@estetica varchar(100) null
AS
BEGIN
	IF(NOT EXISTS (SELECT * FROM dbo.Automovil A WHERE A.Folio=@folio))
	BEGIN
		Raiserror('NO PUEDE ACTUALIZAR ESE REGISTRO',16,1)
	END
	ELSE
	BEGIN
		UPDATE dbo.Automovil SET Marca = @marca,
							     Modelo = @modelo,
								 Color = @color,
								 TipoTransmision = @tipotransmision,
								 Estetica = @estetica
								 WHERE Folio=@folio								 
								 PRINT 'SE HA ACTUALIZADO LA INFORMACION.'
	END
END
GO
/****** Object:  StoredProcedure [dbo].[InsertaVehiculo]    Script Date: 20/02/2019 04:43:34 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertaVehiculo]
@marca varchar(25),
@modelo varchar(25),
@color varchar(15),
@tipotransmision varchar(15),
@estetica varchar(100)
AS
BEGIN
	IF(EXISTS (SELECT * FROM dbo.Automovil WHERE Marca=@marca AND Modelo=@modelo AND Color=@color AND TipoTransmision=@tipotransmision))
	BEGIN
		RAISERROR('NO PUEDE INSERTAR EL MISMO REGISTRO',16,1)
	END
	ELSE
	BEGIN
		INSERT INTO dbo.Automovil(Marca,Modelo,Color,TipoTransmision,Estetica) VALUES (@marca,@modelo,@color,@tipotransmision,@estetica)
		PRINT 'El coche ha sido registrado'
	END
END

GO
/****** Object:  StoredProcedure [dbo].[ReporteCoches]    Script Date: 20/02/2019 04:43:34 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ReporteCoches]
AS
	SELECT A.Marca,A.Modelo,COUNT(*) AS Existencia FROM dbo.Automovil A
	WHERE A.Color='Blanco' OR A.Color='blanco' GROUP BY A.Marca,A.Modelo,A.Folio
	ORDER BY A.Folio
GO
USE [master]
GO
ALTER DATABASE [Agencia] SET  READ_WRITE 
GO
