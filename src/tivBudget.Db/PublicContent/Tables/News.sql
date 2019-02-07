CREATE TABLE [PublicContent].[News] (
    [ID]          UNIQUEIDENTIFIER CONSTRAINT [DF_News_ID] DEFAULT (newsequentialid()) NOT NULL,
    [Title]       NVARCHAR (50)    NOT NULL,
    [Text]        NVARCHAR (MAX)   NOT NULL,
    [OwnerID]     UNIQUEIDENTIFIER NULL,
    [ItemType]    INT              NOT NULL,
    [IsPublished] BIT              NOT NULL,
    [PublishedOn] DATETIME         NULL,
    [CreatedOn]   DATETIME         CONSTRAINT [DF_News_CreatedOn] DEFAULT (getdate()) NOT NULL,
    [CreatedBy]   NVARCHAR (50)    NOT NULL,
    [ModifiedOn]  DATETIME         NULL,
    [ModifiedBy]  NVARCHAR (50)    NULL,
    [ts]          ROWVERSION       NOT NULL,
    CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_News_Users] FOREIGN KEY ([OwnerID]) REFERENCES [Security].[Users] ([ID])
);

