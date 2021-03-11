CREATE TABLE [freebyTrack].[AccountActualTemplates] (
    [ID]                    UNIQUEIDENTIFIER CONSTRAINT [DF_AccountItemTemplates_ID] DEFAULT (newsequentialid()) NOT NULL,
    [AccountTemplateID]     UNIQUEIDENTIFIER NOT NULL,
    [Description]           NVARCHAR (128)   NOT NULL,
    [IsDeposit]             BIT              NOT NULL,
    [IsDefault]             BIT              NOT NULL,
    [AllowRecurringAmount]  BIT              NOT NULL,
    [AllowRecurringPercent] BIT              NOT NULL,
    [AllowRecurringDay]     BIT              NULL,
    [OwnerID]               UNIQUEIDENTIFIER NULL,
    [CreatedOn]             DATETIME         NOT NULL,
    [CreatedBy]             NVARCHAR (50)    NOT NULL,
    [ModifiedOn]            DATETIME         NULL,
    [ModifiedBy]            NVARCHAR (50)    NULL,
    [ts]                    ROWVERSION       NOT NULL,
    CONSTRAINT [PK_AccountActualTemplates] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AccountActualTemplates_AccountTemplates] FOREIGN KEY ([AccountTemplateID]) REFERENCES [freebyTrack].[AccountTemplates] ([ID]),
    CONSTRAINT [FK_AccountActualTemplates_Users] FOREIGN KEY ([OwnerID]) REFERENCES [Security].[Users] ([ID]),
    CONSTRAINT [UC_AccountActualTemplates] UNIQUE NONCLUSTERED ([AccountTemplateID] ASC, [OwnerID] ASC, [Description] ASC)
);

