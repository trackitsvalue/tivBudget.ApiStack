-- =============================================
-- Author:		Eun Eby
-- Create date: 08-07-2014
-- Description:	Budget Category Budget
-- =============================================
CREATE PROCEDURE [Reports].[Budgeting_CategoriesForDateRange_00]
	@UserId UNIQUEIDENTIFIER,
	@FromDate DATE,
	@EndDate DATE
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
 
    
	DECLARE @StartYearMoth VARCHAR(6)=(CAST(YEAR(@FromDate) AS CHAR(4)) + RIGHT('00' + LTRIM(MONTH(@FromDate)),2)) 
	DECLARE @EndYearMonth VARCHAR(6)=(CAST(YEAR(@EndDate) AS CHAR(4)) + RIGHT('00' + LTRIM(MONTH(@EndDate)),2)) 

	SELECT
		BT.[Year],
		BT.[Month],
		BT.[EstimatedSpending],
		'TOTAL' AS [Description],
		CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER) AS CategoryTemplateID
	FROM [freebyTrack].[Budgets] BT
	WHERE BT.OwnerID=@UserId 
		AND CAST(BT.[Year] as char(4)) + RIGHT('00' + LTRIM(BT.[Month]),2) BETWEEN @StartYearMoth AND @EndYearMonth 
	UNION ALL
	SELECT
		B.[Year],
		B.[Month],
		BC.[CategorySpent],
		BCT.[Description],
		BC.CategoryTemplateID
	FROM [freebyTrack].[Budgets] B
	JOIN [freebyTrack].[BudgetCategories] BC 
		ON B.ID=BC.BudgetID
	JOIN [freebyTrack].[BudgetCategoryTemplates] BCT
		ON BCT.ID=BC.CategoryTemplateID
	WHERE B.OwnerID=@UserId
		AND CAST([Year] as char(4)) + RIGHT('00' + LTRIM([Month]),2) BETWEEN @StartYearMoth AND @EndYearMonth
	ORDER BY [Year] ASC,[Month] ASC,CategoryTemplateID ASC

END


