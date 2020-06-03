CREATE TABLE [dbo].[Type] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (50)  NOT NULL,
    [Detail]     NVARCHAR (500) NULL,
    [Status]     INT            CONSTRAINT [DF_Type_Status] DEFAULT ((1)) NOT NULL,
    [CreateDate] DATETIME       CONSTRAINT [DF_Type_CreateDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Type] PRIMARY KEY CLUSTERED ([Id] ASC)
);

