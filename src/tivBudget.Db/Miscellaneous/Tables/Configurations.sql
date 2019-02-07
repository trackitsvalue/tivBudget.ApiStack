CREATE TABLE [Miscellaneous].[Configurations] (
    [ApplicationID] INT            NOT NULL,
    [Name]          NVARCHAR (50)  NOT NULL,
    [Value]         NVARCHAR (MAX) NOT NULL,
    [CreatedOn]     DATETIME       NOT NULL,
    [CreatedBy]     NVARCHAR (50)  NOT NULL,
    [ModifiedOn]    DATETIME       NULL,
    [ModifiedBy]    NVARCHAR (50)  NULL,
    [ts]            ROWVERSION     NOT NULL,
    CONSTRAINT [PK_Configurations] PRIMARY KEY CLUSTERED ([ApplicationID] ASC, [Name] ASC),
    CONSTRAINT [FK_Configurations_Applications] FOREIGN KEY ([ApplicationID]) REFERENCES [Lookups].[Applications] ([ID])
);

