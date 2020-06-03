CREATE TABLE [dbo].[Department] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (50)  NOT NULL,
    [Detail]     NVARCHAR (500) NULL,
    [Status]     INT            CONSTRAINT [DF_Department_Status] DEFAULT ((1)) NOT NULL,
    [CreateDate] DATETIME       CONSTRAINT [DF_Department_CreateDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED ([Id] ASC)
);

