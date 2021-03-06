USE [master]
GO
/****** Object:  Database [RFID]    Script Date: 5/2/2020 11:18:04 PM ******/
CREATE DATABASE [RFID]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RFID', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\RFID.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RFID_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\RFID_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [RFID] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RFID].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RFID] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RFID] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RFID] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RFID] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RFID] SET ARITHABORT OFF 
GO
ALTER DATABASE [RFID] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RFID] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RFID] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RFID] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RFID] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RFID] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RFID] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RFID] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RFID] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RFID] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RFID] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RFID] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RFID] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RFID] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RFID] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RFID] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RFID] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RFID] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [RFID] SET  MULTI_USER 
GO
ALTER DATABASE [RFID] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RFID] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RFID] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RFID] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RFID] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [RFID] SET QUERY_STORE = OFF
GO
USE [RFID]
GO
/****** Object:  Table [dbo].[Antinna]    Script Date: 5/2/2020 11:18:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Antinna](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReaderId] [int] NOT NULL,
	[LocationId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Detail] [nvarchar](500) NULL,
	[Type] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Antinna] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Asset]    Script Date: 5/2/2020 11:18:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Asset](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AssetId] [nvarchar](50) NOT NULL,
	[SiteId] [int] NOT NULL,
	[LocationId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[TypeId] [int] NOT NULL,
	[DepartmentId] [int] NULL,
	[SubAsset] [bit] NOT NULL,
	[MultibleSubSelected] [bit] NULL,
	[Name] [nvarchar](50) NOT NULL,
	[TagId] [varchar](10) NULL,
	[PurchaseDate] [datetime] NULL,
	[Cost] [decimal](10, 3) NULL,
	[Details] [nvarchar](500) NULL,
	[SerialNumber] [nchar](10) NULL,
	[Status] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ExpireDate] [datetime] NULL,
	[CurrentLocation] [int] NULL,
 CONSTRAINT [PK_Asset] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssetImage]    Script Date: 5/2/2020 11:18:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssetImage](
	[AssetId] [int] NOT NULL,
	[Path] [varchar](255) NOT NULL,
	[Size] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Status] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_AssetImage] PRIMARY KEY CLUSTERED 
(
	[AssetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 5/2/2020 11:18:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Detail] [nvarchar](500) NULL,
	[Status] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 5/2/2020 11:18:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Detail] [nvarchar](500) NULL,
	[Status] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 5/2/2020 11:18:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SiteId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Detail] [nvarchar](500) NOT NULL,
	[Status] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[TotalAsset] [int] NULL,
	[InAsset] [int] NULL,
	[OutAsset] [int] NULL,
	[MissingAsset] [int] NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movement]    Script Date: 5/2/2020 11:18:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movement](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AssetId] [int] NOT NULL,
	[LocationId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Direction] [char](3) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Movement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reader]    Script Date: 5/2/2020 11:18:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reader](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SiteId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Type] [int] NOT NULL,
	[AntinnaCount] [int] NOT NULL,
	[Detail] [nvarchar](500) NULL,
	[Status] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Reader] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Site]    Script Date: 5/2/2020 11:18:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Site](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[TagId] [nvarchar](10) NULL,
	[Detail] [nvarchar](500) NULL,
	[Street] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[PostalCode] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[Status] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Site] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubAsset]    Script Date: 5/2/2020 11:18:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubAsset](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AssetId] [int] NOT NULL,
	[TagId] [nvarchar](10) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Status] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_SubAsset] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Type]    Script Date: 5/2/2020 11:18:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Type](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Detail] [nvarchar](500) NULL,
	[Status] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Type] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 5/2/2020 11:18:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](50) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](500) NOT NULL,
	[Flag] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[MobileNumber] [varchar](15) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Antinna] ON 

INSERT [dbo].[Antinna] ([Id], [ReaderId], [LocationId], [Name], [Detail], [Type], [Status], [CreatedDate]) VALUES (1, 1, 2, N'UP', N'One', 0, 1, CAST(N'2020-03-25T02:27:10.777' AS DateTime))
SET IDENTITY_INSERT [dbo].[Antinna] OFF
SET IDENTITY_INSERT [dbo].[Asset] ON 

INSERT [dbo].[Asset] ([Id], [AssetId], [SiteId], [LocationId], [CategoryId], [TypeId], [DepartmentId], [SubAsset], [MultibleSubSelected], [Name], [TagId], [PurchaseDate], [Cost], [Details], [SerialNumber], [Status], [CreatedDate], [ExpireDate], [CurrentLocation]) VALUES (5, N'005', 1, 1, 1, 2, 1, 0, NULL, N'A', N'00000001', CAST(N'2020-01-31T00:00:00.000' AS DateTime), CAST(2202.000 AS Decimal(10, 3)), N'255251', N'2252525   ', 1, CAST(N'2020-03-16T06:37:38.310' AS DateTime), CAST(N'2021-11-02T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Asset] ([Id], [AssetId], [SiteId], [LocationId], [CategoryId], [TypeId], [DepartmentId], [SubAsset], [MultibleSubSelected], [Name], [TagId], [PurchaseDate], [Cost], [Details], [SerialNumber], [Status], [CreatedDate], [ExpireDate], [CurrentLocation]) VALUES (6, N'A06', 1, 1, 1, 2, 1, 0, NULL, N'B', N'00000002', CAST(N'2020-12-12T00:00:00.000' AS DateTime), CAST(15.000 AS Decimal(10, 3)), N'22115', N'1515151   ', 1, CAST(N'2020-03-16T07:49:21.943' AS DateTime), CAST(N'2020-12-01T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Asset] ([Id], [AssetId], [SiteId], [LocationId], [CategoryId], [TypeId], [DepartmentId], [SubAsset], [MultibleSubSelected], [Name], [TagId], [PurchaseDate], [Cost], [Details], [SerialNumber], [Status], [CreatedDate], [ExpireDate], [CurrentLocation]) VALUES (7, N'A07', 1, 1, 1, 1, 2, 0, 0, N'C', N'00000003', CAST(N'2020-12-12T00:00:00.000' AS DateTime), CAST(15.000 AS Decimal(10, 3)), N'22115', N'1515151   ', 3, CAST(N'2020-03-16T07:52:46.883' AS DateTime), CAST(N'2020-12-01T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Asset] ([Id], [AssetId], [SiteId], [LocationId], [CategoryId], [TypeId], [DepartmentId], [SubAsset], [MultibleSubSelected], [Name], [TagId], [PurchaseDate], [Cost], [Details], [SerialNumber], [Status], [CreatedDate], [ExpireDate], [CurrentLocation]) VALUES (8, N'A08', 1, 1, 2, 1, 1, 0, 0, N'D', N'00000004', CAST(N'2018-11-30T00:00:00.000' AS DateTime), CAST(22.000 AS Decimal(10, 3)), N'333', N'444       ', 3, CAST(N'2020-03-16T15:15:44.013' AS DateTime), CAST(N'2017-11-29T00:00:00.000' AS DateTime), NULL)
INSERT [dbo].[Asset] ([Id], [AssetId], [SiteId], [LocationId], [CategoryId], [TypeId], [DepartmentId], [SubAsset], [MultibleSubSelected], [Name], [TagId], [PurchaseDate], [Cost], [Details], [SerialNumber], [Status], [CreatedDate], [ExpireDate], [CurrentLocation]) VALUES (14, N'A1010', 1, 5, 2, 1, NULL, 0, 0, N'Ala Obeidat', N'00000006', NULL, CAST(0.000 AS Decimal(10, 3)), NULL, N'01123251  ', 1, CAST(N'2020-03-26T17:11:20.740' AS DateTime), NULL, 5)
INSERT [dbo].[Asset] ([Id], [AssetId], [SiteId], [LocationId], [CategoryId], [TypeId], [DepartmentId], [SubAsset], [MultibleSubSelected], [Name], [TagId], [PurchaseDate], [Cost], [Details], [SerialNumber], [Status], [CreatedDate], [ExpireDate], [CurrentLocation]) VALUES (15, N'A1010', 1, 5, 1, 1, NULL, 0, 0, N'Ala Obeidat', NULL, NULL, CAST(0.000 AS Decimal(10, 3)), NULL, N'444       ', 1, CAST(N'2020-03-26T17:13:07.983' AS DateTime), NULL, 5)
SET IDENTITY_INSERT [dbo].[Asset] OFF
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Id], [Name], [Detail], [Status], [CreatedDate]) VALUES (1, N'Sales', N'This is for sales items', 1, CAST(N'2020-03-07T14:23:03.923' AS DateTime))
INSERT [dbo].[Category] ([Id], [Name], [Detail], [Status], [CreatedDate]) VALUES (2, N'Visit', N'This for visitor', 1, CAST(N'2020-03-07T14:23:50.167' AS DateTime))
SET IDENTITY_INSERT [dbo].[Category] OFF
SET IDENTITY_INSERT [dbo].[Department] ON 

INSERT [dbo].[Department] ([Id], [Name], [Detail], [Status], [CreateDate]) VALUES (1, N'IT', N'this', 1, CAST(N'2020-03-08T16:04:22.790' AS DateTime))
INSERT [dbo].[Department] ([Id], [Name], [Detail], [Status], [CreateDate]) VALUES (2, N'HR', N'123', 0, CAST(N'2020-03-16T15:15:44.013' AS DateTime))
SET IDENTITY_INSERT [dbo].[Department] OFF
SET IDENTITY_INSERT [dbo].[Location] ON 

INSERT [dbo].[Location] ([Id], [SiteId], [Name], [Detail], [Status], [CreatedDate], [TotalAsset], [InAsset], [OutAsset], [MissingAsset]) VALUES (1, 1, N'Intrance', N'This is For first door', 1, CAST(N'2020-03-07T14:22:05.087' AS DateTime), 2, 1, 1, NULL)
INSERT [dbo].[Location] ([Id], [SiteId], [Name], [Detail], [Status], [CreatedDate], [TotalAsset], [InAsset], [OutAsset], [MissingAsset]) VALUES (2, 1, N'Exit', N'For Exit', 1, CAST(N'2020-03-07T14:24:16.020' AS DateTime), 1, 0, 1, NULL)
INSERT [dbo].[Location] ([Id], [SiteId], [Name], [Detail], [Status], [CreatedDate], [TotalAsset], [InAsset], [OutAsset], [MissingAsset]) VALUES (5, 1, N'222', N'111', 0, CAST(N'2020-03-18T13:29:08.000' AS DateTime), 1, 1, 0, 0)
SET IDENTITY_INSERT [dbo].[Location] OFF
SET IDENTITY_INSERT [dbo].[Movement] ON 

INSERT [dbo].[Movement] ([Id], [AssetId], [LocationId], [Date], [Direction], [Status]) VALUES (22, 5, 1, CAST(N'2020-03-26T04:38:48.573' AS DateTime), N'IN ', 1)
INSERT [dbo].[Movement] ([Id], [AssetId], [LocationId], [Date], [Direction], [Status]) VALUES (23, 6, 1, CAST(N'2020-03-26T04:39:09.917' AS DateTime), N'IN ', 1)
INSERT [dbo].[Movement] ([Id], [AssetId], [LocationId], [Date], [Direction], [Status]) VALUES (24, 7, 1, CAST(N'2020-03-26T04:39:23.340' AS DateTime), N'IN ', 1)
INSERT [dbo].[Movement] ([Id], [AssetId], [LocationId], [Date], [Direction], [Status]) VALUES (25, 8, 1, CAST(N'2020-03-26T04:39:39.800' AS DateTime), N'IN ', 1)
INSERT [dbo].[Movement] ([Id], [AssetId], [LocationId], [Date], [Direction], [Status]) VALUES (26, 5, 1, CAST(N'2020-03-26T04:39:53.930' AS DateTime), N'OUT', 1)
INSERT [dbo].[Movement] ([Id], [AssetId], [LocationId], [Date], [Direction], [Status]) VALUES (27, 8, 1, CAST(N'2020-03-26T04:40:11.513' AS DateTime), N'OUT', 1)
INSERT [dbo].[Movement] ([Id], [AssetId], [LocationId], [Date], [Direction], [Status]) VALUES (28, 5, 1, CAST(N'2020-03-26T04:40:21.877' AS DateTime), N'IN ', 1)
SET IDENTITY_INSERT [dbo].[Movement] OFF
SET IDENTITY_INSERT [dbo].[Reader] ON 

INSERT [dbo].[Reader] ([Id], [SiteId], [Name], [Type], [AntinnaCount], [Detail], [Status], [CreatedDate]) VALUES (1, 1, N'ST-D01', 0, 1, N'Desktop reader', 1, CAST(N'2020-03-25T02:26:48.690' AS DateTime))
SET IDENTITY_INSERT [dbo].[Reader] OFF
SET IDENTITY_INSERT [dbo].[Site] ON 

INSERT [dbo].[Site] ([Id], [Name], [TagId], [Detail], [Street], [City], [State], [PostalCode], [Country], [Status], [CreatedDate]) VALUES (1, N'Hashim', N'011', N'This is Riyadh test', N'Uroba Road', N'Riyadh', N'Riyadh', N'00966', N'Saudi Arabia', 1, CAST(N'2020-03-07T14:21:11.887' AS DateTime))
INSERT [dbo].[Site] ([Id], [Name], [TagId], [Detail], [Street], [City], [State], [PostalCode], [Country], [Status], [CreatedDate]) VALUES (2, N'Allo-edited', N'011', N'This is Riyadh test
Edited', N'Uroba Road', N'Riyadh', N'Riyadh', NULL, N'Saudi Arabia', 1, CAST(N'2020-03-28T06:27:47.827' AS DateTime))
SET IDENTITY_INSERT [dbo].[Site] OFF
SET IDENTITY_INSERT [dbo].[Type] ON 

INSERT [dbo].[Type] ([Id], [Name], [Detail], [Status], [CreateDate]) VALUES (1, N'Main', N'This for main', 1, CAST(N'2020-03-08T16:03:49.020' AS DateTime))
INSERT [dbo].[Type] ([Id], [Name], [Detail], [Status], [CreateDate]) VALUES (2, N'New Type', N'Test', 0, CAST(N'2020-03-16T06:37:49.470' AS DateTime))
SET IDENTITY_INSERT [dbo].[Type] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [FullName], [Username], [Password], [Flag], [CreateDate], [MobileNumber]) VALUES (1, N'Ala Nader Obeidat', N'ala', N'123', 1, CAST(N'2020-03-28T06:46:07.130' AS DateTime), N'0500213307')
INSERT [dbo].[User] ([Id], [FullName], [Username], [Password], [Flag], [CreateDate], [MobileNumber]) VALUES (2, N'Test User', N'test', N'123', 2, CAST(N'2020-03-28T06:55:57.873' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
ALTER TABLE [dbo].[Antinna] ADD  CONSTRAINT [DF_Antinna_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Asset] ADD  CONSTRAINT [DF_Asset_SubAsset]  DEFAULT ((0)) FOR [SubAsset]
GO
ALTER TABLE [dbo].[Asset] ADD  CONSTRAINT [DF_Asset_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[AssetImage] ADD  CONSTRAINT [DF_AssetImage_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [DF_Category_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Department] ADD  CONSTRAINT [DF_Department_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Department] ADD  CONSTRAINT [DF_Department_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Location] ADD  CONSTRAINT [DF_Location_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Movement] ADD  CONSTRAINT [DF_Movement_Date]  DEFAULT (getdate()) FOR [Date]
GO
ALTER TABLE [dbo].[Reader] ADD  CONSTRAINT [DF_Reader_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Site] ADD  CONSTRAINT [DF_Site_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[SubAsset] ADD  CONSTRAINT [DF_SubAsset_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Type] ADD  CONSTRAINT [DF_Type_Status]  DEFAULT ((1)) FOR [Status]
GO
ALTER TABLE [dbo].[Type] ADD  CONSTRAINT [DF_Type_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Antinna]  WITH CHECK ADD  CONSTRAINT [FK_Antinna_Location] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([Id])
GO
ALTER TABLE [dbo].[Antinna] CHECK CONSTRAINT [FK_Antinna_Location]
GO
ALTER TABLE [dbo].[Antinna]  WITH CHECK ADD  CONSTRAINT [FK_Antinna_Reader] FOREIGN KEY([ReaderId])
REFERENCES [dbo].[Reader] ([Id])
GO
ALTER TABLE [dbo].[Antinna] CHECK CONSTRAINT [FK_Antinna_Reader]
GO
ALTER TABLE [dbo].[Asset]  WITH CHECK ADD  CONSTRAINT [FK_Asset_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[Asset] CHECK CONSTRAINT [FK_Asset_Category]
GO
ALTER TABLE [dbo].[Asset]  WITH CHECK ADD  CONSTRAINT [FK_Asset_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO
ALTER TABLE [dbo].[Asset] CHECK CONSTRAINT [FK_Asset_Department]
GO
ALTER TABLE [dbo].[Asset]  WITH CHECK ADD  CONSTRAINT [FK_Asset_Location] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([Id])
GO
ALTER TABLE [dbo].[Asset] CHECK CONSTRAINT [FK_Asset_Location]
GO
ALTER TABLE [dbo].[Asset]  WITH CHECK ADD  CONSTRAINT [FK_Asset_Location1] FOREIGN KEY([CurrentLocation])
REFERENCES [dbo].[Location] ([Id])
GO
ALTER TABLE [dbo].[Asset] CHECK CONSTRAINT [FK_Asset_Location1]
GO
ALTER TABLE [dbo].[Asset]  WITH CHECK ADD  CONSTRAINT [FK_Asset_Site] FOREIGN KEY([SiteId])
REFERENCES [dbo].[Site] ([Id])
GO
ALTER TABLE [dbo].[Asset] CHECK CONSTRAINT [FK_Asset_Site]
GO
ALTER TABLE [dbo].[Asset]  WITH CHECK ADD  CONSTRAINT [FK_Asset_Type] FOREIGN KEY([TypeId])
REFERENCES [dbo].[Type] ([Id])
GO
ALTER TABLE [dbo].[Asset] CHECK CONSTRAINT [FK_Asset_Type]
GO
ALTER TABLE [dbo].[AssetImage]  WITH CHECK ADD  CONSTRAINT [FK_AssetImage_Asset] FOREIGN KEY([AssetId])
REFERENCES [dbo].[Asset] ([Id])
GO
ALTER TABLE [dbo].[AssetImage] CHECK CONSTRAINT [FK_AssetImage_Asset]
GO
ALTER TABLE [dbo].[Location]  WITH CHECK ADD  CONSTRAINT [FK_Location_Site] FOREIGN KEY([SiteId])
REFERENCES [dbo].[Site] ([Id])
GO
ALTER TABLE [dbo].[Location] CHECK CONSTRAINT [FK_Location_Site]
GO
ALTER TABLE [dbo].[Movement]  WITH CHECK ADD  CONSTRAINT [FK_Movement_Asset] FOREIGN KEY([AssetId])
REFERENCES [dbo].[Asset] ([Id])
GO
ALTER TABLE [dbo].[Movement] CHECK CONSTRAINT [FK_Movement_Asset]
GO
ALTER TABLE [dbo].[Movement]  WITH CHECK ADD  CONSTRAINT [FK_Movement_Location] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([Id])
GO
ALTER TABLE [dbo].[Movement] CHECK CONSTRAINT [FK_Movement_Location]
GO
ALTER TABLE [dbo].[Reader]  WITH CHECK ADD  CONSTRAINT [FK_Reader_Site] FOREIGN KEY([SiteId])
REFERENCES [dbo].[Site] ([Id])
GO
ALTER TABLE [dbo].[Reader] CHECK CONSTRAINT [FK_Reader_Site]
GO
ALTER TABLE [dbo].[SubAsset]  WITH CHECK ADD  CONSTRAINT [FK_SubAsset_Asset] FOREIGN KEY([AssetId])
REFERENCES [dbo].[Asset] ([Id])
GO
ALTER TABLE [dbo].[SubAsset] CHECK CONSTRAINT [FK_SubAsset_Asset]
GO
/****** Object:  StoredProcedure [dbo].[r_Asset_List]    Script Date: 5/2/2020 11:18:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Ala
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[r_Asset_List] 
	-- Add the parameters for the stored procedure here
	@pageSize int, 
	@PageNumber int,
	@Id  nvarchar(50)  =null,
    @Name nvarchar(50) = null,
    @Status int = null,
    @TagId nvarchar(10) =null,
	@From datetime =null,
	@To datetime =null,
    @SortColumn int = null,
	@SortDirection int = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT DISTINCT dbo.Asset.Id AS Id, 
		   dbo.Asset.AssetId AS AssetId, 
		   dbo.Asset.Name AS AssetName, 
		   dbo.Asset.Status AS AssetStatus, 
		   dbo.Asset.TagId AS TagId, 
		   dbo.Asset.CreatedDate AS [Date_and_Time], 
		   dbo.AssetImage.Path,
		   dbo.Category.Name as 'Category',
		   dbo.Department.Name as 'Department',
		   dbo.Type.Name as 'Type',
		   dbo.Asset.Cost,
		   dbo.Asset.PurchaseDate,
		   dbo.Location.Name as 'CurrentLocation'
	FROM   dbo.Asset 
			 JOIN  dbo.Category ON dbo.Asset.CategoryId = dbo.Category.Id 	
		   JOIN  dbo.Type ON dbo.Asset.TypeId = dbo.Type.Id 	
		  LEFT JOIN  dbo.Department ON dbo.Asset.DepartmentId = dbo.Department.Id 	
		  LEFT JOIN  dbo.Location ON dbo.Asset.CurrentLocation = dbo.Location.Id 	
		   LEFT JOIN  dbo.AssetImage ON dbo.Asset.Id = dbo.AssetImage.AssetId 
	WHERE (@Id IS NULL OR dbo.Asset.AssetId=@Id)
	  AND (@Name IS NULL OR dbo.Asset.Name LIKE @Name+'%')
	  AND (@Status IS NULL OR dbo.Asset.Status=@Status)
	  AND (@TagId IS NULL OR dbo.Asset.TagId LIKE @TagId+'%')
	  AND (@From IS NULL OR dbo.Asset.CreatedDate >= @From)
	  AND (@To IS NULL OR dbo.Asset.CreatedDate <= @To)
	ORDER BY dbo.Asset.Id
	OFFSET ( @pageSize * (@PageNumber - 1)) ROWS
	FETCH NEXT @pageSize ROWS ONLY;
	--IF @SortDirection = 0
	--BEGIN
	--	SELECT dbo.Asset.Id AS AssetId, 
	--		   dbo.Asset.Name AS AssetName, 
	--		   dbo.Asset.Status AS AssetStatus, 
	--		   dbo.Asset.TagId AS TagId, 
	--		   dbo.Asset.CreatedDate AS [Date_and_Time], 
	--		   dbo.AssetImage.Path,
	--		   dbo.Category.Name,
	--		   dbo.Department.Name,
	--		   dbo.Type.Name,
	--		   dbo.Asset.Cost,
	--		   dbo.Asset.PurchaseDate
	--	FROM   dbo.Asset 
	--			JOIN  dbo.Category ON dbo.Asset.CategoryId = dbo.Category.Id 	
	--			JOIN  dbo.Department ON dbo.Asset.DepartmentId = dbo.Department.Id 	
	--			JOIN  dbo.Type ON dbo.Asset.TypeId = dbo.Type.Id 	
	--		   LEFT JOIN  dbo.AssetImage ON dbo.Asset.Id = dbo.AssetImage.AssetId 
	--	WHERE (@Id IS NULL OR dbo.Asset.Id=@Id)
	--	  AND (@Name IS NULL OR dbo.Asset.Name LIKE @Name+'%')
	--	  AND (@Status IS NULL OR dbo.Asset.Status=@Status)
	--	  AND (@TagId IS NULL OR dbo.Asset.TagId LIKE @TagId+'%')
	--	  AND (@From IS NULL OR dbo.Asset.CreatedDate >= @From)
	--	  AND (@To IS NULL OR dbo.Asset.CreatedDate <= @To)
	--	ORDER BY 
	--	CASE  
	--		WHEN @SortColumn=0 THEN dbo.Asset.Id
	--		WHEN @SortColumn=1 THEN dbo.Asset.Name
	--		WHEN @SortColumn=2 THEN dbo.Asset.Status
	--		WHEN @SortColumn=3 THEN dbo.Asset.TagId
	--		WHEN @SortColumn=4 THEN dbo.Asset.CreatedDate  
	--	END ASC
	--	OFFSET (@PageNumber - 1) ROWS
	--	FETCH NEXT @pageSize ROWS ONLY;
	--END
	--ELSE
	--BEGIN
	--	SELECT dbo.Asset.Id AS AssetId, 
	--		   dbo.Asset.Name AS AssetName, 
	--		   dbo.Asset.Status AS AssetStatus, 
	--		   dbo.Asset.TagId AS TagId, 
	--		   dbo.Asset.CreatedDate AS [Date_and_Time], 
	--		   dbo.AssetImage.Path,
	--		   dbo.Category.Name,
	--		   dbo.Department.Name,
	--		   dbo.Type.Name,
	--		   dbo.Asset.Cost,
	--		   dbo.Asset.PurchaseDate
	--	FROM   dbo.Asset 
	--			JOIN  dbo.Category ON dbo.Asset.CategoryId = dbo.Category.Id 	
	--			JOIN  dbo.Department ON dbo.Asset.DepartmentId = dbo.Department.Id 	
	--			JOIN  dbo.Type ON dbo.Asset.TypeId = dbo.Type.Id 	
	--		   LEFT JOIN  dbo.AssetImage ON dbo.Asset.Id = dbo.AssetImage.AssetId  
	--	WHERE (@Id IS NULL OR dbo.Asset.Id=@Id)
	--	  AND (@Name IS NULL OR dbo.Asset.Name LIKE @Name+'%')
	--	  AND (@Status IS NULL OR dbo.Asset.Status=@Status)
	--	  AND (@TagId IS NULL OR dbo.Asset.TagId LIKE @TagId+'%')
	--	  AND (@From IS NULL OR dbo.Asset.CreatedDate >= @From)
	--	  AND (@To IS NULL OR dbo.Asset.CreatedDate <= @To)
	--	ORDER BY 
	--	CASE  
	--		WHEN @SortColumn=0 THEN dbo.Asset.Id
	--		WHEN @SortColumn=1 THEN dbo.Asset.Name
	--		WHEN @SortColumn=2 THEN dbo.Asset.Status
	--		WHEN @SortColumn=3 THEN dbo.Asset.TagId
	--		WHEN @SortColumn=4 THEN dbo.Asset.CreatedDate  
	--	END DESC
	--	OFFSET (@PageNumber - 1) ROWS
	--	FETCH NEXT @pageSize ROWS ONLY;
	--END

	SELECT  COUNT(1)  as 'RowsCount'
	FROM   dbo.Asset LEFT JOIN  dbo.AssetImage ON dbo.Asset.Id = dbo.AssetImage.AssetId 
		WHERE (@Id IS NULL OR dbo.Asset.AssetId=@Id)
		  AND (@Name IS NULL OR dbo.Asset.Name LIKE @Name+'%')
		  AND (@Status IS NULL OR dbo.Asset.Status=@Status)
		  AND (@TagId IS NULL OR dbo.Asset.TagId LIKE @TagId+'%')
		  AND (@From IS NULL OR dbo.Asset.CreatedDate >= @From)
		  AND (@To IS NULL OR dbo.Asset.CreatedDate <= @To)

END
GO
/****** Object:  StoredProcedure [dbo].[r_AssetTransaction_List]    Script Date: 5/2/2020 11:18:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ala
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[r_AssetTransaction_List] 
	-- Add the parameters for the stored procedure here
	@pageSize int, 
	@PageNumber int,
	@Id nvarchar(50) =null,
    @Name nvarchar(50) = null,
    @Status int = null,
    @Location int =null,
    @Direction varchar(10) = null,
	@From datetime =null,
	@To datetime =null,
    @SortColumn int = null,
	@SortDirection int = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


	 
		SELECT dbo.Asset.AssetId AS AssetId, 
			   dbo.Asset.Name AS AssetName, 
			   dbo.Asset.Status AS AssetStatus, 
			   dbo.Location.Name AS Location, 
			   dbo.Movement.Date AS [Date_and_Time], 
			   dbo.Movement.Direction
		FROM   dbo.Asset INNER JOIN  dbo.Movement ON dbo.Asset.Id = dbo.Movement.AssetId 
						 INNER JOIN  dbo.Location ON dbo.Movement.LocationId = dbo.Location.Id
		WHERE (@Id IS NULL OR dbo.Asset.AssetId=@Id)
		  AND (@Name IS NULL OR dbo.Asset.Name LIKE @Name+'%')
		  AND (@Status IS NULL OR dbo.Asset.Status=@Status)
		  AND (@Location IS NULL OR dbo.Location.Id=@Location)
		  AND (@Direction IS NULL OR dbo.Movement.Direction=@Direction)
		  AND (@From IS NULL OR dbo.Movement.Date >= @From)
		  AND (@To IS NULL OR dbo.Movement.Date <= @To)
		ORDER BY Movement.Id desc
		OFFSET ( @pageSize * (@PageNumber - 1)) ROWS
		FETCH NEXT @pageSize ROWS ONLY;

	SELECT COUNT(1)  as 'RowsCount'
	FROM   dbo.Asset INNER JOIN  dbo.Movement ON dbo.Asset.Id = dbo.Movement.AssetId 
					 INNER JOIN  dbo.Location ON dbo.Movement.LocationId = dbo.Location.Id
	WHERE (@Id IS NULL OR dbo.Asset.AssetId=@Id)
		  AND (@Name IS NULL OR dbo.Asset.Name LIKE @Name+'%')
		  AND (@Status IS NULL OR dbo.Asset.Status=@Status)
		  AND (@Location IS NULL OR dbo.Location.Id=@Location)
		  AND (@Direction IS NULL OR dbo.Movement.Direction=@Direction)
		  AND (@From IS NULL OR dbo.Movement.Date >= @From)
		  AND (@To IS NULL OR dbo.Movement.Date <= @To)

END
GO
/****** Object:  StoredProcedure [dbo].[r_Movement_Insert]    Script Date: 5/2/2020 11:18:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[r_Movement_Insert] 
@TID nvarchar(10),
@AntinnaId int,
@Direction char(3),
@Status int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @AssetId int =(SELECT [Id] FROM [Asset] WHERE TagId=@TID)
	DECLARE @LocationId int =(SELECT [LocationId] FROM [Antinna] WHERE Id=@AntinnaId)
	IF @Direction='IN '
	BEGIN
		UPDATE [Asset]
		SET CurrentLocation = @LocationId
		WHERE Id=@AssetId
	END
	ELSE
	BEGIN
		UPDATE [Asset]
		SET CurrentLocation = NULL
		WHERE Id=@AssetId
	END
INSERT INTO [dbo].[Movement]
           ([AssetId]
           ,[LocationId]
           ,[Direction]
           ,[Status])
     VALUES
           (@AssetId
           ,@LocationId
           ,@Direction
           ,@Status)
END
GO
USE [master]
GO
ALTER DATABASE [RFID] SET  READ_WRITE 
GO
