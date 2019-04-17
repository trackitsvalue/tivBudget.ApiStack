CREATE TABLE [freebyTrack].[Accounts] (
    [ID]                       UNIQUEIDENTIFIER CONSTRAINT [DF_Accounts_ID] DEFAULT (newsequentialid()) NOT NULL,
    [AccountTemplateID]        UNIQUEIDENTIFIER NOT NULL,
    [AccountTypeID]            INT              NOT NULL,
    [Description]              NVARCHAR (128)   NOT NULL,
    [OwnerID]                  UNIQUEIDENTIFIER NOT NULL,
    [AreAccountCategoriesOpen] BIT              NOT NULL,
    [DisplayIndex]             INT              NOT NULL,
    [CreatedOn]                DATETIME         NOT NULL,
    [CreatedBy]                NVARCHAR (50)    NOT NULL,
    [ModifiedOn]               DATETIME         NULL,
    [ModifiedBy]               NVARCHAR (50)    NULL,
    [ts]                       ROWVERSION       NOT NULL,
    [IsEnabled] BIT NOT NULL DEFAULT (1), 
    [IsDefaultOfType] BIT NOT NULL DEFAULT (0), 
    CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Accounts_AccountTemplates] FOREIGN KEY ([AccountTemplateID]) REFERENCES [freebyTrack].[AccountTemplates] ([ID]),
    CONSTRAINT [FK_Accounts_AccountTypes] FOREIGN KEY ([AccountTypeID]) REFERENCES [freebyTrack].[AccountTypes] ([ID]),
    CONSTRAINT [FK_Accounts_Users] FOREIGN KEY ([OwnerID]) REFERENCES [Security].[Users] ([ID])
);

