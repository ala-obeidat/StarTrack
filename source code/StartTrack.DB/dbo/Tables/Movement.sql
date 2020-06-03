CREATE TABLE [dbo].[Movement] (
    [Id]         INT      IDENTITY (1, 1) NOT NULL,
    [AssetId]    INT      NOT NULL,
    [LocationId] INT      NOT NULL,
    [Date]       DATETIME CONSTRAINT [DF_Movement_Date] DEFAULT (getdate()) NOT NULL,
    [Direction]  CHAR (3) NOT NULL,
    [Status]     INT      NOT NULL,
    CONSTRAINT [PK_Movement] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Movement_Asset] FOREIGN KEY ([AssetId]) REFERENCES [dbo].[Asset] ([Id]),
    CONSTRAINT [FK_Movement_Location] FOREIGN KEY ([LocationId]) REFERENCES [dbo].[Location] ([Id])
);

