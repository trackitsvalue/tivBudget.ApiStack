CREATE TABLE [Security].[UserSettings] (
    [ID]            UNIQUEIDENTIFIER CONSTRAINT [DF_UserSettings_ID] DEFAULT (newsequentialid()) NOT NULL,
	[UserID]        UNIQUEIDENTIFIER NOT NULL,
    [ApplicationID] INT              NOT NULL,
    [Name]          NVARCHAR (50)    NOT NULL,
    [Value]         NVARCHAR (MAX)   NOT NULL,
    [IsCacheable]   BIT              CONSTRAINT [DF_Security.UserSettings_IsCachable] DEFAULT ((1)) NOT NULL,
    [IsWritable]    BIT              CONSTRAINT [DF_Security.UserSettings_IsWritable] DEFAULT ((1)) NOT NULL,
    [CreatedOn]     DATETIME         NOT NULL,
    [CreatedBy]     NVARCHAR (50)    NOT NULL,
    [ModifiedOn]    DATETIME         NULL,
    [ModifiedBy]    NVARCHAR (50)    NULL,
    [ts]            ROWVERSION       NOT NULL,
    CONSTRAINT [PK_UserSettings] PRIMARY KEY NONCLUSTERED ([ID] ASC),
    CONSTRAINT [FK_UserSettings_Applications] FOREIGN KEY ([ApplicationID]) REFERENCES [Lookups].[Applications] ([ID]),
    CONSTRAINT [FK_UserSettings_Users] FOREIGN KEY ([UserID]) REFERENCES [Security].[Users] ([ID])
);

GO
CREATE CLUSTERED INDEX [CI_UserSettings]
    ON [Security].[UserSettings]([Name] ASC, [UserID] ASC, [ApplicationID] ASC);
