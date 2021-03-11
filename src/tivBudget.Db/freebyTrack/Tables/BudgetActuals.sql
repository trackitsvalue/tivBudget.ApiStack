CREATE TABLE [freebyTrack].[BudgetActuals] (
    [ID]                UNIQUEIDENTIFIER CONSTRAINT [DF_BudgetActuals_ID] DEFAULT (newsequentialid()) NOT NULL,
    [Description]       NVARCHAR (256)   NOT NULL,
    [Amount]            DECIMAL (18, 2)  NOT NULL,
    [RelevantOn]        DATE             NOT NULL,
    [ItemID]            UNIQUEIDENTIFIER NOT NULL,
    [IsLinked]          BIT              CONSTRAINT [DF_BudgetActuals_IsLinked] DEFAULT (0) NOT NULL,
    [IsEnvelopeDeposit] BIT              NOT NULL,
    [DisplayIndex]      INT              NOT NULL,
    [CreatedOn]         DATETIME         NOT NULL,
    [CreatedBy]         NVARCHAR (50)    NOT NULL,
    [ModifiedOn]        DATETIME         NULL,
    [ModifiedBy]        NVARCHAR (50)    NULL,
    [ts]                ROWVERSION       NOT NULL,
    [AccountLinkID]         UNIQUEIDENTIFIER NULL,
    [AccountCategoryLinkID] UNIQUEIDENTIFIER NULL,
    [IsCreditWithdrawl] BIT NOT NULL CONSTRAINT [DF_BudgetActuals_IsCreditWithdrawl] DEFAULT (0), 
    CONSTRAINT [PK_BudgetActuals] PRIMARY KEY NONCLUSTERED ([ID] ASC),
    CONSTRAINT [FK_BudgetActuals_AccountCategories] FOREIGN KEY ([AccountCategoryLinkID]) REFERENCES [freebyTrack].[AccountCategories] ([ID]),
    CONSTRAINT [FK_BudgetActuals_Accounts] FOREIGN KEY ([AccountLinkID]) REFERENCES [freebyTrack].[Accounts] ([ID]),
	CONSTRAINT [FK_BudgetActuals_BudgetItems] FOREIGN KEY ([ItemID]) REFERENCES [freebyTrack].[BudgetItems] ([ID]) ON DELETE CASCADE
);


GO
CREATE CLUSTERED INDEX [CI_BudgetActualsItem]
    ON [freebyTrack].[BudgetActuals]([ItemID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_BudgetActuals_RelevantOn]
    ON [freebyTrack].[BudgetActuals]([RelevantOn] ASC);

