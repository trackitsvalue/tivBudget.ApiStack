-- =============================================
-- Author:		Eun Eby
-- Create date: 08-07-2014
-- Description:	Get Line Item by Category
-- =============================================
CREATE PROCEDURE [Reports].[Budgeting_ItemsInCategoryForDateRange_00]
	@UserId UNIQUEIDENTIFIER,
	@CategoryTemplateId UNIQUEIDENTIFIER,
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
	WHERE BCT.[ID]=@CategoryTemplateId
     
	DECLARE @StartYearMoth VARCHAR(6)=(CAST(YEAR(@StartDate) AS CHAR(4)) + RIGHT('00' + LTRIM(MONTH(@StartDate)),2)) 
	DECLARE @EndYearMonth VARCHAR(6)=(CAST(YEAR(@EndDate) AS CHAR(4)) + RIGHT('00' + LTRIM(MONTH(@EndDate)),2)) 
    
	SELECT 
		B.[Year],
		B.[Month],
		BCT.[Description],
		BC.CategorySpent AS Spent,
		CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER) AS ID
	FROM [freebyTrack].[BudgetCategoryTemplates] BCT 
		JOIN [freebyTrack].[BudgetCategories] BC
			ON BCT.[ID]=BC.CategoryTemplateID
		JOIN [freebyTrack].[Budgets] B
			ON B.ID=BC.BudgetID
	WHERE B.OwnerID=@UserId 
		AND BCT.[ID]=@CategoryTemplateId
		AND CAST(B.[Year] as char(4)) + RIGHT('00' + LTRIM(B.[Month]),2) BETWEEN @StartYearMoth AND @EndYearMonth 
	UNION ALL
	SELECT 
		B.[Year],
		B.[Month],
		BT.[Description],
		SUM(BI.[ItemSpent]) AS Spent,
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
		AND BCT.[ID]=@CategoryTemplateId
		AND CAST(B.[Year] as char(4)) + RIGHT('00' + LTRIM(B.[Month]),2) BETWEEN @StartYearMoth AND @EndYearMonth 
	GROUP BY B.[Year], B.[Month],BT.[Description], BT.[ID]
	ORDER BY [Year],[Month], ID
	
END


