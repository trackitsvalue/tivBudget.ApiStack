CREATE TABLE [freebyTrack].[BudgetCategories] (
    [ID]                 UNIQUEIDENTIFIER CONSTRAINT [DF_BudgetCategories_ID] DEFAULT (newsequentialid()) NOT NULL,
    [CategoryTemplateID] UNIQUEIDENTIFIER NOT NULL,
    [Description]        NVARCHAR (128)   NOT NULL,
    [AreBudgetItemsOpen] BIT              NOT NULL,
    [BudgetID]           UNIQUEIDENTIFIER NOT NULL,
    [DisplayIndex]       INT              NOT NULL,
    [CategoryBudgeted]   DECIMAL (18, 2)  NOT NULL,
    [CategorySpent]      DECIMAL (18, 2)  NOT NULL,
    [CategoryRemaining]  DECIMAL (18, 2)  NOT NULL,
    [CreatedOn]          DATETIME         NOT NULL,
    [CreatedBy]          NVARCHAR (50)    NOT NULL,
    [ModifiedOn]         DATETIME         NULL,
    [ModifiedBy]         NVARCHAR (50)    NULL,
    [ts]                 ROWVERSION       NOT NULL,
    CONSTRAINT [PK_BudgetCategories] PRIMARY KEY NONCLUSTERED ([ID] ASC),
    CONSTRAINT [FK_BudgetCategories_Budget] FOREIGN KEY ([BudgetID]) REFERENCES [freebyTrack].[Budgets] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_BudgetCategories_BudgetCategoryTemplates] FOREIGN KEY ([CategoryTemplateID]) REFERENCES [freebyTrack].[BudgetCategoryTemplates] ([ID])
);


GO
CREATE CLUSTERED INDEX [CI_BudgetCategoriesBudget]
    ON [freebyTrack].[BudgetCategories]([BudgetID] ASC);

