CREATE TABLE [dbo].[AssetImage] (
    [AssetId]     INT           NOT NULL,
    [Path]        VARCHAR (255) NOT NULL,
    [Size]        INT           NOT NULL,
    [Name]        NVARCHAR (50) NOT NULL,
    [Status]      INT           NOT NULL,
    [CreatedDate] DATETIME      CONSTRAINT [DF_AssetImage_CreatedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_AssetImage] PRIMARY KEY CLUSTERED ([AssetId] ASC),
    CONSTRAINT [FK_AssetImage_Asset] FOREIGN KEY ([AssetId]) REFERENCES [dbo].[Asset] ([Id])
);

