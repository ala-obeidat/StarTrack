CREATE TABLE [dbo].[Site] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50)  NOT NULL,
    [TagId]       NVARCHAR (10)  NULL,
    [Detail]      NVARCHAR (500) NULL,
    [Street]      NVARCHAR (50)  NULL,
    [City]        NVARCHAR (50)  NULL,
    [State]       NVARCHAR (50)  NULL,
    [PostalCode]  NVARCHAR (50)  NULL,
    [Country]     NVARCHAR (50)  NULL,
    [Status]      INT            NOT NULL,
    [CreatedDate] DATETIME       CONSTRAINT [DF_Site_CreatedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Site] PRIMARY KEY CLUSTERED ([Id] ASC)
);

