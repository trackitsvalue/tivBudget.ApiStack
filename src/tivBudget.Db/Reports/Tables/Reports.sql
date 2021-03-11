CREATE TABLE [Reports].[Reports] (
    [ID]              UNIQUEIDENTIFIER CONSTRAINT [DF__Reports__ID__02E7657A] DEFAULT (newsequentialid()) NOT NULL,
    [CategoryID]      INT              NOT NULL,
    [Description]     NVARCHAR (100)   NOT NULL,
    [DisplayIndex]    INT              NOT NULL,
    [StoredProcedure] NVARCHAR (50)    NOT NULL,
    [ReportHelper]    NVARCHAR (200)   NOT NULL,
    [CreatedOn]       DATETIME         NOT NULL,
    [CreatedBy]       NVARCHAR (50)    NOT NULL,
    [ModifiedOn]      DATETIME         NULL,
    [ModifiedBy]      NVARCHAR (50)    NULL,
    [ts]              ROWVERSION       NOT NULL,
    CONSTRAINT [PK_Reports] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Reports_ReportCategories] FOREIGN KEY ([CategoryID]) REFERENCES [Lookups].[ReportCategories] ([ID])
);

