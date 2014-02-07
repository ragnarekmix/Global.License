USE [master]
GO
/****** Object:  Database [License]    Script Date: 02/07/2014 14:55:20 ******/
CREATE DATABASE [License] ON  PRIMARY 
( NAME = N'License', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\License.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'License_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\License_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [License] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [License].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [License] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [License] SET ANSI_NULLS OFF
GO
ALTER DATABASE [License] SET ANSI_PADDING OFF
GO
ALTER DATABASE [License] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [License] SET ARITHABORT OFF
GO
ALTER DATABASE [License] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [License] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [License] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [License] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [License] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [License] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [License] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [License] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [License] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [License] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [License] SET  DISABLE_BROKER
GO
ALTER DATABASE [License] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [License] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [License] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [License] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [License] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [License] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [License] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [License] SET  READ_WRITE
GO
ALTER DATABASE [License] SET RECOVERY FULL
GO
ALTER DATABASE [License] SET  MULTI_USER
GO
ALTER DATABASE [License] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [License] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'License', N'ON'
GO
USE [License]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 02/07/2014 14:55:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Licenses]    Script Date: 02/07/2014 14:55:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Licenses](
	[LicenseId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[Key] [nvarchar](100) NOT NULL,
	[CreationDate] [date] NOT NULL,
	[Modification] [nvarchar](100) NULL,
 CONSTRAINT [PK_Licenses] PRIMARY KEY CLUSTERED 
(
	[LicenseId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_Licenses_Customers]    Script Date: 02/07/2014 14:55:21 ******/
ALTER TABLE [dbo].[Licenses]  WITH CHECK ADD  CONSTRAINT [FK_Licenses_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([CustomerId])
GO
ALTER TABLE [dbo].[Licenses] CHECK CONSTRAINT [FK_Licenses_Customers]
GO
/****** Insert [Customers] Script Date: 02/07/2014 14:55:21 ******/
INSERT INTO [dbo].[Customers] VALUES ('Mihail', 'Podobivsky');
INSERT INTO [dbo].[Customers] VALUES ('Joe', 'O"Raely');
/****** Insert [Licenses] Script Date: 02/07/2014 14:55:21 ******/
INSERT INTO [dbo].[Licenses] VALUES (1, 'lasdjflksdf', '20110202', '');
INSERT INTO [dbo].[Licenses] VALUES (1, 'asdfsadfads', '20120202', '');
INSERT INTO [dbo].[Licenses] VALUES (1, 'sdfgsdfgfgd', '20130202', '');
INSERT INTO [dbo].[Licenses] VALUES (2, 'ghjfhgkfjhk', '20110202', '');
INSERT INTO [dbo].[Licenses] VALUES (2, 'grytutyjytj', '20120202', '');
INSERT INTO [dbo].[Licenses] VALUES (2, 'vmhvmgydyjh', '20130202', '');