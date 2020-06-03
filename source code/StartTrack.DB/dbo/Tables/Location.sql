CREATE TABLE [dbo].[Location] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [SiteId]       INT            NOT NULL,
    [TagId]        NVARCHAR (10)  NOT NULL,
    [Name]         NVARCHAR (50)  NOT NULL,
    [Detail]       NVARCHAR (500) NOT NULL,
    [Status]       INT            NOT NULL,
    [CreatedDate]  DATETIME       CONSTRAINT [DF_Location_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [TotalAsset]   INT            NULL,
    [InAsset]      INT            NULL,
    [OutAsset]     INT            NULL,
    [MissingAsset] INT            NULL,
    CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Location_Site] FOREIGN KEY ([SiteId]) REFERENCES [dbo].[Site] ([Id])
);

