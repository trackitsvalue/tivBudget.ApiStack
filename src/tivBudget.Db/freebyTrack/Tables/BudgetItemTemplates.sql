CREATE TABLE [freebyTrack].[BudgetItemTemplates] (
    [ID]                            UNIQUEIDENTIFIER CONSTRAINT [DF_BudgetItemTemplates_ID] DEFAULT (newsequentialid()) NOT NULL,
    [CategoryTemplateID]            UNIQUEIDENTIFIER NOT NULL,
    [Description]                   NVARCHAR (128)   NOT NULL,
    [OwnerID]                       UNIQUEIDENTIFIER NULL,
    [IsVirtualType]                 BIT              NOT NULL CONSTRAINT [DF_BudgetItemTemplates_IsVirtualType] DEFAULT (0),
	[IsAccountTransferType]         BIT              NOT NULL CONSTRAINT [DF_BudgetItemTemplates_IsAccountTransferType] DEFAULT (0),
    [IsCreditAllowed]               BIT              NOT NULL CONSTRAINT [DF_BudgetItemTemplates_IsCreditAllowed] DEFAULT (1),
    [IsEnvelopeAllowed]             BIT              NOT NULL CONSTRAINT [DF_BudgetItemTemplates_IsEnvelopeAllowed] DEFAULT (1),
    [CreatedOn]                     DATETIME         NOT NULL,
    [CreatedBy]                     NVARCHAR (50)    NOT NULL,
    [ModifiedOn]                    DATETIME         NULL,
    [ModifiedBy]                    NVARCHAR (50)    NULL,
    [ts]                            ROWVERSION       NOT NULL,	
    [AllowedAccountLinkTypesOverride] NVARCHAR(40) NOT NULL CONSTRAINT [DF_BudgetItemTemplates_AllowedAccountLinkTypesOverride] DEFAULT (N''), 
    CONSTRAINT [PK_BudgetItemTemplates] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_BudgetItemTemplates_BudgetCategoryTemplates] FOREIGN KEY ([CategoryTemplateID]) REFERENCES [freebyTrack].[BudgetCategoryTemplates] ([ID]),
    CONSTRAINT [FK_BudgetItemTemplates_Users] FOREIGN KEY ([OwnerID]) REFERENCES [Security].[Users] ([ID]),
    CONSTRAINT [UC_BudgetItemTemplates] UNIQUE NONCLUSTERED ([CategoryTemplateID] ASC, [OwnerID] ASC, [Description] ASC)
);

