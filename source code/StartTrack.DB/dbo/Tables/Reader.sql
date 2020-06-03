CREATE TABLE [dbo].[Reader] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [SiteId]       INT            NOT NULL,
    [Name]         NVARCHAR (50)  NOT NULL,
    [Type]         INT            NOT NULL,
    [AntinnaCount] INT            NOT NULL,
    [Detail]       NVARCHAR (500) NULL,
    [Status]       INT            NOT NULL,
    [CreatedDate]  DATETIME       CONSTRAINT [DF_Reader_CreatedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Reader] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Reader_Site] FOREIGN KEY ([SiteId]) REFERENCES [dbo].[Site] ([Id])
);

