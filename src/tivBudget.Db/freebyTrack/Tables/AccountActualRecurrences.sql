CREATE TABLE [freebyTrack].[AccountActualRecurrences] (
    [ID]               UNIQUEIDENTIFIER CONSTRAINT [DF_AccountActualRecurrences_ID] DEFAULT (newsequentialid()) NOT NULL,
    [ActualTemplateID] UNIQUEIDENTIFIER NOT NULL,
    [AccountID]        UNIQUEIDENTIFIER NOT NULL,
    [Description]      NVARCHAR (256)   NOT NULL,
    [RelevantDay]      INT              NULL,
    [Amount]           DECIMAL (18, 2)  NULL,
    [Percent]          DECIMAL (5, 2)   NULL,
    [CreatedOn]        DATETIME         NOT NULL,
    [CreatedBy]        NVARCHAR (50)    NOT NULL,
    [ModifiedOn]       DATETIME         NULL,
    [ModifiedBy]       NVARCHAR (50)    NULL,
    [ts]               ROWVERSION       NOT NULL,
    CONSTRAINT [PK_AccountActualRecurrences] PRIMARY KEY NONCLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AccountActualRecurrences_AccountActualTemplates] FOREIGN KEY ([ActualTemplateID]) REFERENCES [freebyTrack].[AccountActualTemplates] ([ID]),
    CONSTRAINT [FK_AccountActualRecurrences_Accounts] FOREIGN KEY ([AccountID]) REFERENCES [freebyTrack].[Accounts] ([ID]) ON DELETE CASCADE
);

