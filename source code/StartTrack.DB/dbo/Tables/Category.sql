CREATE TABLE [dbo].[Category] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50)  NOT NULL,
    [Detail]      NVARCHAR (500) NULL,
    [Status]      INT            NOT NULL,
    [CreatedDate] DATETIME       CONSTRAINT [DF_Category_CreatedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED ([Id] ASC)
);

