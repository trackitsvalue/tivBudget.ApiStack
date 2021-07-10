CREATE TABLE [Security].[UserAccomplishments] (
    [ID]            UNIQUEIDENTIFIER CONSTRAINT [DF_UserAccomplishments_ID] DEFAULT (newsequentialid()) NOT NULL,
	[UserID]        UNIQUEIDENTIFIER NOT NULL,
    [ApplicationID] INT              NOT NULL,
    [Type]			NVARCHAR (50)    NOT NULL,
    [SubType]		NVARCHAR (50)    NOT NULL,
    [AssociatedID]  UNIQUEIDENTIFIER NOT NULL,
	[EarnedExperience] INT			 NOT NULL,
    [Icon]			NVARCHAR (50)    NOT NULL,
    [Title]         NVARCHAR (128)   NOT NULL,
	[Description]   NVARCHAR (MAX)   NOT NULL,
	[IsAcknowledged] BIT             NOT NULL,
    [CreatedOn]     DATETIME         NOT NULL,
    [CreatedBy]     NVARCHAR (50)    NOT NULL,
    [ModifiedOn]    DATETIME         NULL,
    [ModifiedBy]    NVARCHAR (50)    NULL,
    [ts]            ROWVERSION       NOT NULL,
    CONSTRAINT [PK_UserAccomplishments] PRIMARY KEY NONCLUSTERED ([ID] ASC),
    CONSTRAINT [FK_UserAccomplishments_Applications] FOREIGN KEY ([ApplicationID]) REFERENCES [Lookups].[Applications] ([ID]),
    CONSTRAINT [FK_UserAccomplishments_Users] FOREIGN KEY ([UserID]) REFERENCES [Security].[Users] ([ID])
);

GO
CREATE CLUSTERED INDEX [CI_UserAccomplishments]
    ON [Security].[UserAccomplishments]([UserID] ASC, [ApplicationID] ASC, [Type] ASC, [SubType] ASC, [AssociatedID] ASC);