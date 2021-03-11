-- =============================================
-- Author:		Eun Eby
-- Create date: 08-07-2014
-- Description:	Get Line Item by Category
--
-- Updated:		James Eby / 8/15/2014
-- Description:	Updated format to be useful for
--				HighCharts.
-- =============================================
create PROCEDURE [Reports].[Budgeting_ItemsInCategoryForDateRange_01]
	@UserId UNIQUEIDENTIFIER,
	@BudgetCategoryTemplateId UNIQUEIDENTIFIER,
	@StartDate DATE,
	@EndDate DATE
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
 
	DECLARE @Subtitle AS NVARCHAR(128);
	SET @Subtitle = 'From ' + DATENAME(m, @StartDate) + ' of ' + DATENAME(yyyy, @StartDate) + ' to ' + DATENAME(m, @EndDate) + ' of ' + DATENAME(yyyy, @EndDate)
 
	SELECT 'line' as ChartType, BCT.[Description] + ' Spending Breakdown' as Title, @SubTitle as Subtitle
	FROM [freebyTrack].[BudgetCategoryTemplates] BCT 
	WHERE BCT.[ID]=@BudgetCategoryTemplateId
     
	DECLARE @StartYearMoth NVARCHAR(6)=(CAST(YEAR(@StartDate) AS NCHAR(4)) + RIGHT('00' + LTRIM(MONTH(@StartDate)),2)) 
	DECLARE @EndYearMonth NVARCHAR(6)=(CAST(YEAR(@EndDate) AS NCHAR(4)) + RIGHT('00' + LTRIM(MONTH(@EndDate)),2)) 
    
	SELECT 
		CAST(B.[Year] as nchar(4)) + RIGHT('00' + LTRIM(B.[Month]),2) as Sort,
		CAST(B.[Month] as nvarchar(2)) + '/' + CAST(B.[Year] as nchar(4)) as Category,
		BCT.[Description],
		BC.CategorySpent AS Amount,
		CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER) AS ID
	FROM [freebyTrack].[BudgetCategoryTemplates] BCT 
		JOIN [freebyTrack].[BudgetCategories] BC
			ON BCT.[ID]=BC.CategoryTemplateID
		JOIN [freebyTrack].[Budgets] B
			ON B.ID=BC.BudgetID
	WHERE B.OwnerID=@UserId 
		AND BCT.[ID]=@BudgetCategoryTemplateId
		AND CAST(B.[Year] as nchar(4)) + RIGHT('00' + LTRIM(B.[Month]),2) BETWEEN @StartYearMoth AND @EndYearMonth 
	UNION ALL
	SELECT 
		CAST(B.[Year] as nchar(4)) + RIGHT('00' + LTRIM(B.[Month]),2) as Sort,
		CAST(B.[Month] as nvarchar(2)) + '/' + CAST(B.[Year] as nchar(4)) as Category,
		BT.[Description],
		SUM(BI.[ItemSpent]) AS Amount,
		BT.ID 
	FROM [freebyTrack].[BudgetCategoryTemplates] BCT 
		JOIN [freebyTrack].[BudgetCategories] BC
			ON BCT.[ID]=BC.CategoryTemplateID
		JOIN [freebyTrack].[BudgetItems] BI
			ON BI.CategoryID=BC.[ID]
		JOIN [freebyTrack].[BudgetItemTemplates] BT
			ON BI.ItemTemplateID=BT.[ID]
		JOIN [freebyTrack].[Budgets] B
			ON B.ID=BC.BudgetID
	WHERE B.OwnerID=@UserId 
		AND BCT.[ID]=@BudgetCategoryTemplateId
		AND CAST(B.[Year] as nchar(4)) + RIGHT('00' + LTRIM(B.[Month]),2) BETWEEN @StartYearMoth AND @EndYearMonth 
	GROUP BY CAST(B.[Year] as nchar(4)) + RIGHT('00' + LTRIM(B.[Month]),2),
		CAST(B.[Month] as nvarchar(2)) + '/' + CAST(B.[Year] as nchar(4)),
		BT.[Description], BT.[ID]
	ORDER BY Sort, ID
	
END


