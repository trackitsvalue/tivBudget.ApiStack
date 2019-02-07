IF NOT EXISTS (SELECT TOP 1 Id FROM Security.Groups)
BEGIN
	INSERT INTO [Security].[Groups] ([ID], [Description], [IsNewUserDefault], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (1, N'Free Users', 1, '20140310 21:55:00.000', N'James', NULL, NULL)
	INSERT INTO [Security].[Groups] ([ID], [Description], [IsNewUserDefault], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (2, N'Beta Users', 0, '20140310 21:56:00.000', N'James', NULL, NULL)
END