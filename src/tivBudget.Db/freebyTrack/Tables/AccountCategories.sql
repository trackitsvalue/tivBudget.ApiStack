CREATE TABLE [freebyTrack].[AccountCategories] (
    [ID]                    UNIQUEIDENTIFIER CONSTRAINT [DF_AccountCategories_ID] DEFAULT (newsequentialid()) NOT NULL,
    [CategoryTemplateID]    UNIQUEIDENTIFIER NOT NULL,
    [Description]           NVARCHAR (128)   NOT NULL,
    [AreAccountActualsOpen] BIT              NOT NULL,
    [AccountID]             UNIQUEIDENTIFIER NOT NULL,
    [DisplayIndex]          INT              NOT NULL,
    [IsDefault]             BIT              NOT NULL,
    [CreatedOn]             DATETIME         NOT NULL,
    [CreatedBy]             NVARCHAR (50)    NOT NULL,
    [ModifiedOn]            DATETIME         NULL,
    [ModifiedBy]            NVARCHAR (50)    NULL,
    [ts]                    ROWVERSION       NOT NULL,
    CONSTRAINT [PK_AccountCategories] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AccountCategories_AccountCategoryTemplates] FOREIGN KEY ([CategoryTemplateID]) REFERENCES [freebyTrack].[AccountCategoryTemplates] ([ID]),
    CONSTRAINT [FK_AccountCategories_Accounts] FOREIGN KEY ([AccountID]) REFERENCES [freebyTrack].[Accounts] ([ID]) ON DELETE CASCADE
);

