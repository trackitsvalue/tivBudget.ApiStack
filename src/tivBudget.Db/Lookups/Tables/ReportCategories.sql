CREATE TABLE [Lookups].[ReportCategories] (
    [ID]           INT           IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (50) NOT NULL,
    [DisplayIndex] INT           NOT NULL,
    [CreatedOn]    DATETIME      NOT NULL,
    [CreatedBy]    NVARCHAR (50) NOT NULL,
    [ModifiedOn]   DATETIME      NULL,
    [ModifiedBy]   NVARCHAR (50) NULL,
    [ts]           ROWVERSION    NOT NULL,
    CONSTRAINT [PK_ReportCategories] PRIMARY KEY CLUSTERED ([ID] ASC)
);

