IF NOT EXISTS (SELECT TOP 1 Id FROM Lookups.ReportCategories)
BEGIN
	SET IDENTITY_INSERT [Lookups].[ReportCategories] ON
	INSERT INTO [Lookups].[ReportCategories] ([ID], [Name], [DisplayIndex], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (1, N'Budget Reports', 0, '20140807 12:03:00.000', N'James', NULL, NULL)
	INSERT INTO [Lookups].[ReportCategories] ([ID], [Name], [DisplayIndex], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (2, N'Account Reports', 1, '20140807 12:03:00.000', N'James', NULL, NULL)
	SET IDENTITY_INSERT [Lookups].[ReportCategories] OFF
END