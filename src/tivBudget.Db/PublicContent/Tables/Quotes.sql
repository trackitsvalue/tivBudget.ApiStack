CREATE TABLE [PublicContent].[Quotes] (
    [ID]        INT             NOT NULL,
    [Source]    NVARCHAR (50)   NOT NULL,
    [Text]      NVARCHAR (1024) NOT NULL,
    [CreatedOn] DATETIME        CONSTRAINT [DF_Quotes_CreatedOn] DEFAULT (getdate()) NOT NULL,
    [CreatedBy] NVARCHAR (50)   NOT NULL,
    [ModifiedOn] DATETIME NULL, 
    [ModifiedBy] NVARCHAR(50) NULL, 
    [ts] ROWVERSION NOT NULL, 
    CONSTRAINT [PK_Quotes] PRIMARY KEY CLUSTERED ([ID] ASC)
);

