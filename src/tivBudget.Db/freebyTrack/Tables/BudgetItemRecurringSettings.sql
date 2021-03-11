CREATE TABLE [freebyTrack].[BudgetItemRecurringSettings] (
    [ID]               UNIQUEIDENTIFIER CONSTRAINT [DF_BudgetItemRecurringSettings_ID] DEFAULT (newsequentialid()) NOT NULL,
    [DayDue]           INT              NULL,
    [FriendlyReminder] INT              NULL,
    [WarningReminder]  INT              NULL,
    [SendViaEmail]     BIT              NOT NULL,
    [SendViaText]      BIT              NOT NULL,
    [CreatedOn]        DATETIME         NOT NULL,
    [CreatedBy]        NVARCHAR (50)    NOT NULL,
    [ModifiedOn]       DATETIME         NULL,
    [ModifiedBy]       NVARCHAR (50)    NULL,
    [ts]               ROWVERSION       NOT NULL,
    CONSTRAINT [PK_BudgetItemRecurringSettings] PRIMARY KEY CLUSTERED ([ID] ASC)
);

