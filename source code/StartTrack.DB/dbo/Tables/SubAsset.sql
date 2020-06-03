CREATE TABLE [dbo].[SubAsset] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [AssetId]     INT           NOT NULL,
    [TagId]       NVARCHAR (10) NOT NULL,
    [Name]        NVARCHAR (50) NOT NULL,
    [Status]      INT           NOT NULL,
    [CreatedDate] DATETIME      CONSTRAINT [DF_SubAsset_CreatedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_SubAsset] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SubAsset_Asset] FOREIGN KEY ([AssetId]) REFERENCES [dbo].[Asset] ([Id])
);

