CREATE TABLE [freebyTrack].[AccountCategoryTemplates] (
    [ID]                UNIQUEIDENTIFIER CONSTRAINT [DF_AccountCategoryTemplates_ID] DEFAULT (newsequentialid()) NOT NULL,
    [AccountTemplateID] UNIQUEIDENTIFIER NOT NULL,
    [Description]       NVARCHAR (128)   NOT NULL,
    [IsDefault]         BIT              NOT NULL,
    [OwnerID]           UNIQUEIDENTIFIER NULL,
    [Icon]              NVARCHAR (50)    NOT NULL,
    [BackgroundColor]   NCHAR (9)        NOT NULL,
    [CreatedOn]         DATETIME         NOT NULL,
    [CreatedBy]         NVARCHAR (50)    NOT NULL,
    [ModifiedOn]        DATETIME         NULL,
    [ModifiedBy]        NVARCHAR (50)    NULL,
    [ts]                ROWVERSION       NOT NULL,
    CONSTRAINT [PK_AccountCategoryTemplates] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AccountCategoryTemplates_AccountTemplates] FOREIGN KEY ([AccountTemplateID]) REFERENCES [freebyTrack].[AccountTemplates] ([ID]),
    CONSTRAINT [FK_AccountCategoryTemplates_Users] FOREIGN KEY ([OwnerID]) REFERENCES [Security].[Users] ([ID]),
    CONSTRAINT [UC_AccountCategoryTemplates] UNIQUE NONCLUSTERED ([AccountTemplateID] ASC, [OwnerID] ASC, [Description] ASC)
);

