CREATE TABLE [freebyTrack].[AccountTemplates] (
    [ID]              UNIQUEIDENTIFIER CONSTRAINT [DF_AccountTemplates_ID] DEFAULT (newsequentialid()) NOT NULL,
    [TypeID]          INT              NOT NULL,
    [Description]     NVARCHAR (128)   NOT NULL,
    [OwnerID]         UNIQUEIDENTIFIER NULL,
    [Icon]            NVARCHAR (50)    NOT NULL,
    [IsIncomeAccount] BIT              NOT NULL,
    [CreatedOn]       DATETIME         NOT NULL,
    [CreatedBy]       NVARCHAR (50)    NOT NULL,
    [ModifiedOn]      DATETIME         NULL,
    [ModifiedBy]      NVARCHAR (50)    NULL,
    [ts]              ROWVERSION       NOT NULL,
    CONSTRAINT [PK_AccountTemplates] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AccountTemplates_AccountTypes] FOREIGN KEY ([TypeID]) REFERENCES [freebyTrack].[AccountTypes] ([ID]),
    CONSTRAINT [FK_AccountTemplates_Users] FOREIGN KEY ([OwnerID]) REFERENCES [Security].[Users] ([ID]),
    CONSTRAINT [UC_AccountTemplates] UNIQUE NONCLUSTERED ([OwnerID] ASC, [Description] ASC)
);

