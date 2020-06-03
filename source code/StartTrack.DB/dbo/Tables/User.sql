CREATE TABLE [dbo].[User] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [FullName]     NVARCHAR (50) NOT NULL,
    [Username]     VARCHAR (50)  NOT NULL,
    [Password]     VARCHAR (500) NOT NULL,
    [Flag]         INT           NOT NULL,
    [CreateDate]   DATETIME      CONSTRAINT [DF_User_CreateDate] DEFAULT (getdate()) NOT NULL,
    [MobileNumber] VARCHAR (15)  NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);

