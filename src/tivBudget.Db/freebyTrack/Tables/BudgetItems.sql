CREATE TABLE [freebyTrack].[BudgetItems] (
    [ID]                    UNIQUEIDENTIFIER CONSTRAINT [DF_BudgetItems_ID] DEFAULT (newsequentialid()) NOT NULL,
    [ItemTemplateID]        UNIQUEIDENTIFIER NOT NULL,
    [CategoryID]            UNIQUEIDENTIFIER NOT NULL,
    [Description]           NVARCHAR (128)   NOT NULL,
    [AmountBudgeted]        DECIMAL (18, 2)  NOT NULL,
    [AreBudgetActualsOpen]  BIT              NOT NULL,
    [IsLinked]              BIT              CONSTRAINT [DF_BudgetItems_IsLinked] DEFAULT ((0)) NOT NULL,
    [DisplayIndex]          INT              NOT NULL,
    [ItemSpent]             DECIMAL (18, 2)  NOT NULL,
    [ItemRemaining]         DECIMAL (18, 2)  NOT NULL,
    [AccountLinkID]         UNIQUEIDENTIFIER NULL,
    [AccountCategoryLinkID] UNIQUEIDENTIFIER NULL,
    [RecurringSettingsID]   UNIQUEIDENTIFIER NULL,
    [AlertID]               UNIQUEIDENTIFIER NULL,
    [CreatedOn]             DATETIME         NOT NULL,
    [CreatedBy]             NVARCHAR (50)    NOT NULL,
    [ModifiedOn]            DATETIME         NULL,
    [ModifiedBy]            NVARCHAR (50)    NULL,
    [ts]                    ROWVERSION       NOT NULL,
    CONSTRAINT [PK_BudgetItems] PRIMARY KEY NONCLUSTERED ([ID] ASC),
    CONSTRAINT [FK_BudgetItems_AccountCategories] FOREIGN KEY ([AccountCategoryLinkID]) REFERENCES [freebyTrack].[AccountCategories] ([ID]),
    CONSTRAINT [FK_BudgetItems_Accounts] FOREIGN KEY ([AccountLinkID]) REFERENCES [freebyTrack].[Accounts] ([ID]),
    CONSTRAINT [FK_BudgetItems_BudgetCategories] FOREIGN KEY ([CategoryID]) REFERENCES [freebyTrack].[BudgetCategories] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_BudgetItems_BudgetItemAlerts] FOREIGN KEY ([AlertID]) REFERENCES [freebyTrack].[BudgetItemAlerts] ([ID]),
    CONSTRAINT [FK_BudgetItems_BudgetItemRecurringSettings] FOREIGN KEY ([RecurringSettingsID]) REFERENCES [freebyTrack].[BudgetItemRecurringSettings] ([ID]),
    CONSTRAINT [FK_BudgetItems_BudgetItemTemplates] FOREIGN KEY ([ItemTemplateID]) REFERENCES [freebyTrack].[BudgetItemTemplates] ([ID])
);


GO
CREATE CLUSTERED INDEX [CI_BudgetItemsCategory]
    ON [freebyTrack].[BudgetItems]([CategoryID] ASC);

