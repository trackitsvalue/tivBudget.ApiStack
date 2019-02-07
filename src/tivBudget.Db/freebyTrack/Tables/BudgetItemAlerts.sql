CREATE TABLE [freebyTrack].[BudgetItemAlerts] (
    [ID]           UNIQUEIDENTIFIER CONSTRAINT [DF_BudgetItemAlerts_ID] DEFAULT (newsequentialid()) NOT NULL,
    [AlertType]    INT              NOT NULL,
    [Acknowledged] BIT              NOT NULL,
    [CreatedOn]    DATETIME         NOT NULL,
    [CreatedBy]    NVARCHAR (50)    NOT NULL,
    [ModifiedOn]   DATETIME         NULL,
    [ModifiedBy]   NVARCHAR (50)    NULL,
    [ts]           ROWVERSION       NOT NULL,
    CONSTRAINT [PK_BudgetAlerts] PRIMARY KEY CLUSTERED ([ID] ASC)
);

