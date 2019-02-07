-- =============================================
-- Author:		Eun Eby
-- Create date: 08-07-2014
-- Description:	Get Line Item by Category
--
-- Updated:		Eun & James Eby / 8-15-2014
-- Description:	Updated format to be useful for
--				HighCharts.
--
-- Updated:		James Eby / 8-31-2014
-- Description:	Added more summation data.
-- =============================================
CREATE PROCEDURE [Reports].[Budgeting_ItemsInCategoryForDateRange]
	@UserId UNIQUEIDENTIFIER,
	@BudgetCategoryTemplateId UNIQUEIDENTIFIER,
	@StartDate DATE,
	@EndDate DATE
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
 
	DECLARE @TempBudget AS TABLE(
		BudgetId UNIQUEIDENTIFIER,
		DateKey NVARCHAR(8),
		DisplayDate NVARCHAR(8)) 

	DECLARE @StartYearMoth NVARCHAR(6)=(CAST(YEAR(@StartDate) AS NCHAR(4)) + RIGHT('00' + LTRIM(MONTH(@StartDate)),2)) 
	DECLARE @EndYearMonth NVARCHAR(6)=(CAST(YEAR(@EndDate) AS NCHAR(4)) + RIGHT('00' + LTRIM(MONTH(@EndDate)),2)) 


	--capture records by given userid and insert into temp table
	INSERT INTO @TempBudget
	SELECT 
		ID,  
		CAST([Year] AS NVARCHAR)+ RIGHT('00' + LTRIM([Month]),2),
		RIGHT('00' + LTRIM([Month]),2) +'/' +CAST([Year] AS NVARCHAR)
	FROM [freebyTrack].[Budgets] B
	WHERE OwnerID=@UserId
 
	DECLARE @totalSpent DECIMAL(18, 2)
	SELECT @totalSpent = SUM(BC.CategorySpent)
	FROM [freebyTrack].[BudgetCategoryTemplates] BCT 
		JOIN [freebyTrack].[BudgetCategories] BC
			ON BCT.[ID]=BC.CategoryTemplateID
		JOIN @TempBudget TB
			ON TB.BudgetId=BC.BudgetID
	WHERE BCT.[ID]=@BudgetCategoryTemplateId
		AND TB.DateKey BETWEEN @StartYearMoth AND @EndYearMonth 

	DECLARE @Subtitle AS NVARCHAR(128);
	SET @Subtitle = 'Between ' + DATENAME(m, @StartDate) + ' of ' + DATENAME(yyyy, @StartDate) + ' and ' + DATENAME(m, @EndDate) + ' of ' + DATENAME(yyyy, @EndDate)
 
	DECLARE @bctDescription NVARCHAR(50);
	SELECT @bctDescription = BCT.[Description]
	FROM [freebyTrack].[BudgetCategoryTemplates] BCT 
	WHERE BCT.[ID]=@BudgetCategoryTemplateId
 
	(
	SELECT 'Total Spending for ' + @bctDescription + ' by Month' as Title, @SubTitle as Subtitle, 0 as DisplayIndex
	UNION
	SELECT 'Breakdown of Spending for ' + @bctDescription + ' by Month' as Title, @SubTitle as Subtitle, 1 as DisplayIndex
	UNION
	SELECT 'Overall ' + @bctDescription + ' Spending by Item' as Title, CAST(@totalSpent as NVARCHAR(18)) + ' Spent ' + @SubTitle as Subtitle, 2 as DisplayIndex
	) ORDER BY DisplayIndex
     
    
	SELECT  DateKey, DisplayDate
	FROM @TempBudget T
	WHERE T.DateKey BETWEEN @StartYearMoth AND @EndYearMonth 
	GROUP BY DateKey, DisplayDate

	SELECT 
		TB.DateKey,
		--TB.Category,
		'All ' + BCT.[Description] + ' Spending' as 'Description',
		BC.CategorySpent AS Amount,
		CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER) AS ID
	FROM [freebyTrack].[BudgetCategoryTemplates] BCT 
		JOIN [freebyTrack].[BudgetCategories] BC
			ON BCT.[ID]=BC.CategoryTemplateID
		JOIN @TempBudget TB
			ON TB.BudgetId=BC.BudgetID
	WHERE BCT.[ID]=@BudgetCategoryTemplateId
		AND TB.DateKey BETWEEN @StartYearMoth AND @EndYearMonth 
	
	
	SELECT 
		TB.DateKey,
		--TB.Category,
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
		JOIN @TempBudget TB
			ON TB.BudgetId=BC.BudgetID
	WHERE BCT.[ID]=@BudgetCategoryTemplateId
		AND TB.DateKey BETWEEN @StartYearMoth AND @EndYearMonth
	GROUP BY TB.DateKey, BT.[Description], BT.[ID]
	ORDER BY BT.ID, TB.DateKey
	

END


