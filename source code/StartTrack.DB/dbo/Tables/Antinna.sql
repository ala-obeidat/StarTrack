CREATE TABLE [dbo].[Antinna] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [ReaderId]    INT            NOT NULL,
    [LocationId]  INT            NOT NULL,
    [Name]        NVARCHAR (50)  NOT NULL,
    [Detail]      NVARCHAR (500) NULL,
    [Type]        INT            NOT NULL,
    [Status]      INT            NOT NULL,
    [CreatedDate] DATETIME       CONSTRAINT [DF_Antinna_CreatedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Antinna] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Antinna_Location] FOREIGN KEY ([LocationId]) REFERENCES [dbo].[Location] ([Id]),
    CONSTRAINT [FK_Antinna_Reader] FOREIGN KEY ([ReaderId]) REFERENCES [dbo].[Reader] ([Id])
);

