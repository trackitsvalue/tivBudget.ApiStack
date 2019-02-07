CREATE TABLE [Reports].[ReportControls] (
    [ID]           UNIQUEIDENTIFIER CONSTRAINT [DF__ReportContro__ID__08A03ED0] DEFAULT (newsequentialid()) NOT NULL,
    [ReportID]     UNIQUEIDENTIFIER NOT NULL,
    [DisplayIndex] INT              NOT NULL,
    [Description]  NVARCHAR (50)    NOT NULL,
    [Html]         NVARCHAR (MAX)   NULL,
    [CreatedOn]    DATETIME         NOT NULL,
    [CreatedBy]    NVARCHAR (50)    NOT NULL,
    [ModifiedOn]   DATETIME         NULL,
    [ModifiedBy]   NVARCHAR (50)    NULL,
    [ts]           ROWVERSION       NOT NULL,
    CONSTRAINT [PK_ReportControls] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ReportControls_Reports] FOREIGN KEY ([ReportID]) REFERENCES [Reports].[Reports] ([ID])
);

