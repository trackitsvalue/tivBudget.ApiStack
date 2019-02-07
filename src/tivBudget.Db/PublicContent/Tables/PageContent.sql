CREATE TABLE [PublicContent].[PageContent] (
    [ID]          UNIQUEIDENTIFIER CONSTRAINT [DF_PageContent_ID] DEFAULT (newsequentialid()) NOT NULL,
    [PageName]    NVARCHAR (50)    NOT NULL,
    [PageSection] NVARCHAR (50)    NOT NULL,
    [HtmlContent] NVARCHAR (MAX)   NOT NULL,
    [Version]     INT              NOT NULL,
    [IsPublished] BIT              NOT NULL,
    [PublishedOn] DATETIME         NULL,
    [CreatedOn]   DATETIME         CONSTRAINT [DF_PageContent_CreatedOn] DEFAULT (getdate()) NOT NULL,
    [CreatedBy]   NVARCHAR (50)    NOT NULL,
    [ModifiedOn]  DATETIME         NULL,
    [ModifiedBy]  NVARCHAR (50)    NULL,
    [ts]          ROWVERSION       NOT NULL,
    CONSTRAINT [PK_PageContent] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [IX_PageContent] UNIQUE NONCLUSTERED ([PageName] ASC, [PageSection] ASC, [Version] ASC)
);

