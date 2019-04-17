CREATE TABLE [freebyTrack].[BudgetCategoryTemplates] (
    [ID]               UNIQUEIDENTIFIER CONSTRAINT [DF_BudgetCategoryTemplates_ID] DEFAULT (newsequentialid()) NOT NULL,
    [Description]      NVARCHAR (128)   NOT NULL,
    [OwnerID]          UNIQUEIDENTIFIER NULL,
    [Icon]             NVARCHAR (50)    NOT NULL,
    [IsIncomeCategory] BIT              NOT NULL,
    [BackgroundColor]  NCHAR (9)        NOT NULL,
    [CreatedOn]        DATETIME         NOT NULL,
    [CreatedBy]        NVARCHAR (50)    NOT NULL,
    [ModifiedOn]       DATETIME         NULL,
    [ModifiedBy]       NVARCHAR (50)    NULL,
    [ts]               ROWVERSION       NOT NULL,
    [AllowedAccountLinkTypes] NVARCHAR(40) NOT NULL DEFAULT (N'|1|2|3|4|5|6|7|8|'), 
    [IsSystemCategory] BIT NOT NULL DEFAULT (0), 
    CONSTRAINT [PK_BudgetCategoryTemplates] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_BudgetCategoryTemplates_Users] FOREIGN KEY ([OwnerID]) REFERENCES [Security].[Users] ([ID]),
    CONSTRAINT [UC_BudgetCategoryTemplates] UNIQUE NONCLUSTERED ([OwnerID] ASC, [Description] ASC)
);

