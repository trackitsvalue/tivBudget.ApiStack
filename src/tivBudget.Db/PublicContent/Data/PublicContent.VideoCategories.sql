IF NOT EXISTS (SELECT TOP 1 Id FROM PublicContent.VideoCategories)
BEGIN
	INSERT INTO [PublicContent].[VideoCategories] ([ID], [Description], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (N'3d206199-63ec-e311-821b-00215e73190e', N'Monthly Budgeting', '20140604 22:43:48.030', N'James', NULL, NULL)
	INSERT INTO [PublicContent].[VideoCategories] ([ID], [Description], [CreatedOn], [CreatedBy], [ModifiedOn], [ModifiedBy]) VALUES (N'3fb487eb-64ec-e311-821b-00215e73190e', N'Account Management', '20140604 22:53:15.370', N'James', NULL, NULL)
END