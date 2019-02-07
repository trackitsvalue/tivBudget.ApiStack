IF NOT EXISTS (SELECT TOP 1 Id FROM Lookups.Applications)
BEGIN
	INSERT INTO [Lookups].[Applications] ([ID], [Name], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (4, N'trackItsValue API', '20140126 00:48:18.647', N'James', NULL, NULL)
	INSERT INTO [Lookups].[Applications] ([ID], [Name], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (5, N'trackItsValue.com Website', '20140306 16:10:00.000', N'James', NULL, NULL)
	INSERT INTO [Lookups].[Applications] ([ID], [Name], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (6, N'freebyRunner Alerts Task', '20170608 00:30:00.000', N'James', NULL, NULL)
END