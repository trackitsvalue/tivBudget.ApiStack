CREATE TABLE [Security].[UserAccomplishments] (
    [UserID]        UNIQUEIDENTIFIER NOT NULL,
    [ApplicationID] INT              NOT NULL,
    [Type]			NVARCHAR (50)    NOT NULL,
    [SubType]		NVARCHAR (50)    NOT NULL,
    [Icon]			NVARCHAR (50)    NOT NULL,
    [Description]   NVARCHAR (128)   NOT NULL,
	[IsAcknowledged] BIT             NOT NULL,	
    [CreatedOn]     DATETIME         NOT NULL,
    [CreatedBy]     NVARCHAR (50)    NOT NULL,
    [ModifiedOn]    DATETIME         NULL,
    [ModifiedBy]    NVARCHAR (50)    NULL,
    [ts]            ROWVERSION       NOT NULL,
    CONSTRAINT [PK_UserAccomplishments] PRIMARY KEY CLUSTERED ([UserID] ASC, [ApplicationID] ASC, [Type] ASC, [SubType] ASC),
    CONSTRAINT [FK_UserAccomplishments_Applications] FOREIGN KEY ([ApplicationID]) REFERENCES [Lookups].[Applications] ([ID]),
    CONSTRAINT [FK_UserAccomplishments_Users] FOREIGN KEY ([UserID]) REFERENCES [Security].[Users] ([ID])
);