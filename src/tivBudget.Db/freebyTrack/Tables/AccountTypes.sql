CREATE TABLE [freebyTrack].[AccountTypes] (
    [ID]                          INT            NOT NULL,
    [DisplayIndex]                INT            NOT NULL,
    [DescriptionPlural]           NVARCHAR (128) NOT NULL,
    [DescriptionSingular]         NVARCHAR (128) NOT NULL,
    [PosLineItemShortDescription] NVARCHAR (10)  NOT NULL,
    [PosLineItemDescription]      NVARCHAR (50)  NOT NULL,
    [NegLineItemShortDescription] NVARCHAR (10)  NOT NULL,
    [NegLineItemDescription]      NVARCHAR (50)  NOT NULL,
    [CreatedOn]                   DATETIME       NOT NULL,
    [CreatedBy]                   NVARCHAR (128) NOT NULL,
    [ModifiedOn]                  DATETIME       NULL,
    [ModifiedBy]                  NVARCHAR (128) NULL,
    [ts]                          ROWVERSION     NOT NULL,
    CONSTRAINT [PK_AccountTypes] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [UC_AccountTypes_DescriptionPlural] UNIQUE NONCLUSTERED ([DescriptionPlural] ASC)
);

