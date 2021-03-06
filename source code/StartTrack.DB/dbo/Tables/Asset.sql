﻿CREATE TABLE [dbo].[Asset] (
    [Id]                  INT             IDENTITY (1, 1) NOT NULL,
    [AssetId]             NVARCHAR (50)   NOT NULL,
    [SiteId]              INT             NOT NULL,
    [LocationId]          INT             NOT NULL,
    [CategoryId]          INT             NOT NULL,
    [TypeId]              INT             NOT NULL,
    [DepartmentId]        INT             NOT NULL,
    [SubAsset]            BIT             CONSTRAINT [DF_Asset_SubAsset] DEFAULT ((0)) NOT NULL,
    [MultibleSubSelected] BIT             NULL,
    [Name]                NVARCHAR (50)   NOT NULL,
    [TagId]               VARCHAR (10)    NOT NULL,
    [PurchaseDate]        DATETIME        NULL,
    [Cost]                DECIMAL (10, 3) NULL,
    [Details]             NVARCHAR (500)  NULL,
    [SerialNumber]        NCHAR (10)      NULL,
    [Status]              INT             NOT NULL,
    [CreatedDate]         DATETIME        CONSTRAINT [DF_Asset_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [ExpireDate]          DATETIME        NULL,
    CONSTRAINT [PK_Asset] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Asset_Category] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([Id]),
    CONSTRAINT [FK_Asset_Department] FOREIGN KEY ([DepartmentId]) REFERENCES [dbo].[Department] ([Id]),
    CONSTRAINT [FK_Asset_Location] FOREIGN KEY ([LocationId]) REFERENCES [dbo].[Location] ([Id]),
    CONSTRAINT [FK_Asset_Site] FOREIGN KEY ([SiteId]) REFERENCES [dbo].[Site] ([Id]),
    CONSTRAINT [FK_Asset_Type] FOREIGN KEY ([TypeId]) REFERENCES [dbo].[Type] ([Id])
);

