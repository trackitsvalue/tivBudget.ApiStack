-- =============================================
-- Author:		Eun Eby
-- Create date: 08-07-2014
-- Description:	Budget Category Budget
--
-- Updated:		Eun & James Eby / 8-15-2014
-- Description:	Updated format to be useful for
--				HighCharts.
--
-- Updated:		James Eby / 8-31-2014
-- Description:	Added more summation data.
-- =============================================
CREATE PROCEDURE [Reports].[Budgeting_CategoriesForDateRange]
	@UserId UNIQUEIDENTIFIER,
	@StartDate DATE,
	@EndDate DATE
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
 
    
	DECLARE @TempBudget AS TABLE(
		BudgetId UNIQUEIDENTIFIER,
		DateKey NVARCHAR(8),
		DisplayDate NVARCHAR(8),
		ActualSpending DECIMAL(18,2)) 

	DECLARE @StartYearMoth NVARCHAR(6)=(CAST(YEAR(@StartDate) AS NCHAR(4)) + RIGHT('00' + LTRIM(MONTH(@StartDate)),2)) 
	DECLARE @EndYearMonth NVARCHAR(6)=(CAST(YEAR(@EndDate) AS NCHAR(4)) + RIGHT('00' + LTRIM(MONTH(@EndDate)),2)) 


	--capture records by given userid and insert into temp table
	INSERT INTO @TempBudget
	SELECT 
		ID,  
		CAST([Year] AS NVARCHAR)+ RIGHT('00' + LTRIM([Month]),2),
		RIGHT('00' + LTRIM([Month]),2) +'/' +CAST([Year] AS NVARCHAR),
		ActualSpending
	FROM [freebyTrack].[Budgets] B
	WHERE OwnerID=@UserId
 
	DECLARE @totalSpent DECIMAL(18, 2)
	SELECT @totalSpent = SUM(TB.ActualSpending)
	FROM @TempBudget TB
	WHERE TB.DateKey BETWEEN @StartYearMoth AND @EndYearMonth 
 
	DECLARE @Subtitle AS NVARCHAR(128);
	SET @Subtitle = 'Between ' + DATENAME(m, @StartDate) + ' of ' + DATENAME(yyyy, @StartDate) + ' and ' + DATENAME(m, @EndDate) + ' of ' + DATENAME(yyyy, @EndDate)
 
	(
	SELECT 'Total Budget Spending by Month' as Title, @SubTitle as Subtitle, 0 as DisplayIndex
	UNION
	SELECT 'Breakdown of Spending for All Categories by Month' as Title, @SubTitle as Subtitle, 1 as DisplayIndex
	UNION
	SELECT 'Overall Budget Spending by Categeory' as Title, CAST(@totalSpent as NVARCHAR(18)) + ' Spent ' + @SubTitle as Subtitle, 2 as DisplayIndex
	)
	ORDER BY DisplayIndex
 
	SELECT  T.DateKey, T.DisplayDate
	FROM @TempBudget T
	WHERE T.DateKey BETWEEN @StartYearMoth AND @EndYearMonth 
	GROUP BY T.DateKey, T.DisplayDate
	 
	SELECT
		TB.DateKey,
		--TB.Category,
		'All Categories' AS [Description],
		TB.ActualSpending as Amount,
		CAST(CAST(0 AS BINARY) AS UNIQUEIDENTIFIER) AS ID
	FROM @TempBudget TB
	WHERE TB.DateKey BETWEEN @StartYearMoth AND @EndYearMonth 
		
	SELECT
		TB.DateKey,
		--TB.Category,
		BCT.[Description],
		BC.[CategorySpent] AS Amount,
		BC.CategoryTemplateID AS ID
	FROM @TempBudget TB
	JOIN [freebyTrack].[BudgetCategories] BC 
		ON TB.BudgetId=BC.BudgetID
	JOIN [freebyTrack].[BudgetCategoryTemplates] BCT
		ON BCT.ID=BC.CategoryTemplateID
	WHERE TB.DateKey BETWEEN @StartYearMoth AND @EndYearMonth
		AND bct.IsIncomeCategory=0
	ORDER BY ID, DateKey

END


