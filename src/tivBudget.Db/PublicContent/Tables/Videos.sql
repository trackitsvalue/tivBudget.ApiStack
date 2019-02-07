CREATE TABLE [PublicContent].[Videos] (
    [ID]          UNIQUEIDENTIFIER CONSTRAINT [DF__Videos__ID__668030F6] DEFAULT (newsequentialid()) NOT NULL,
    [Title]       NVARCHAR (128)   NOT NULL,
    [VideoEmbed]  NVARCHAR (1024)  NOT NULL,
    [Description] NVARCHAR (1024)  NOT NULL,
    [CategoryID]  UNIQUEIDENTIFIER NOT NULL,
    [CreatedOn]   DATETIME         CONSTRAINT [DF__Videos__CreatedO__6774552F] DEFAULT (getdate()) NOT NULL,
    [CreatedBy]   NVARCHAR (50)    NOT NULL,
    [ModifiedOn]  DATETIME         NULL,
    [ModifiedBy]  NVARCHAR (50)    NULL,
    [ts]          ROWVERSION       NOT NULL,
    CONSTRAINT [PK_Videos] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Videos_VideoCategories] FOREIGN KEY ([CategoryID]) REFERENCES [PublicContent].[VideoCategories] ([ID])
);

