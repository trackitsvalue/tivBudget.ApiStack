CREATE TABLE [Security].[Groups] (
    [ID]               BIGINT        NOT NULL,
    [Description]      NVARCHAR (50) NOT NULL,
    [IsNewUserDefault] BIT           NOT NULL,
    [CreatedOn]        DATETIME      NOT NULL,
    [CreatedBy]        NVARCHAR (50) NOT NULL,
    [ModifiedOn]       DATETIME      NULL,
    [ModifiedBy]       NVARCHAR (50) NULL,
    [ts]               ROWVERSION    NOT NULL,
    CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [UC_GroupsDescription] UNIQUE NONCLUSTERED ([Description] ASC)
);

