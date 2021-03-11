CREATE TABLE [Lookups].[Applications] (
    [ID]         INT           NOT NULL,
    [Name]       NVARCHAR (50) NOT NULL,
    [CreatedOn]  DATETIME      NOT NULL,
    [CreatedBy]  NVARCHAR (50) NOT NULL,
    [ModifiedOn] DATETIME      NULL,
    [ModifiedBy] NVARCHAR (50) NULL,
    [ts]         ROWVERSION    NOT NULL,
    CONSTRAINT [PK_Applications] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [UC_Application_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);

