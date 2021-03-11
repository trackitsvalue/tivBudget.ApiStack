CREATE TABLE [PublicContent].[VideoCategories] (
    [ID]          UNIQUEIDENTIFIER DEFAULT (newsequentialid()) NOT NULL,
    [Description] NVARCHAR (50)    NOT NULL,
    [CreatedOn]   DATETIME         CONSTRAINT [DF_VideoCategories_CreatedOn] DEFAULT (getdate()) NOT NULL,
    [CreatedBy]   NVARCHAR (50)    NOT NULL,
    [ModifiedOn]  DATETIME         NULL,
    [ModifiedBy]  NVARCHAR (50)    NULL,
    [ts]          ROWVERSION       NOT NULL,
    CONSTRAINT [PK_VideoCategories] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [IX_VideoCategories] UNIQUE NONCLUSTERED ([Description] ASC)
);

