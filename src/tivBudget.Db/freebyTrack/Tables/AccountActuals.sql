CREATE TABLE [freebyTrack].[AccountActuals] (
    [ID]                 UNIQUEIDENTIFIER CONSTRAINT [DF_AccountActuals_ID] DEFAULT (newsequentialid()) NOT NULL,
    [ActualTemplateID]   UNIQUEIDENTIFIER NOT NULL,
    [BudgetActualLinkID] UNIQUEIDENTIFIER NULL,
    [CategoryID]         UNIQUEIDENTIFIER NOT NULL,
    [Description]        NVARCHAR (256)   NOT NULL,
    [RelevantOn]         DATE             NOT NULL,
    [Amount]             DECIMAL (18, 2)  NOT NULL,
    [IsLinked]           BIT              NOT NULL,
    [IsRecurring]        BIT              NOT NULL,
    [CreatedOn]          DATETIME         NOT NULL,
    [CreatedBy]          NVARCHAR (50)    NOT NULL,
    [ModifiedOn]         DATETIME         NULL,
    [ModifiedBy]         NVARCHAR (50)    NULL,
    [ts]                 ROWVERSION       NOT NULL,
    [IsBudgetDefaultLink] BIT CONSTRAINT [DF_AccountActuals_IsBudgetDefaultLink] DEFAULT (0) NOT NULL , 
    CONSTRAINT [PK_AccountActuals] PRIMARY KEY NONCLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AccountActuals_AccountActualTemplates] FOREIGN KEY ([ActualTemplateID]) REFERENCES [freebyTrack].[AccountActualTemplates] ([ID]),
    CONSTRAINT [FK_AccountActuals_AccountCategories] FOREIGN KEY ([CategoryID]) REFERENCES [freebyTrack].[AccountCategories] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_AccountActuals_BudgetActuals] FOREIGN KEY ([BudgetActualLinkID]) REFERENCES [freebyTrack].[BudgetActuals] ([ID]) ON DELETE CASCADE
);

