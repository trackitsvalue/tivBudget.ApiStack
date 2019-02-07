-- =============================================
-- Author:		Eun Eby
-- Create date: 9/3/2014
-- Description:	Retreives the running surplus or
-- deficit for budgets within the given date
-- range.
-- =============================================
CREATE PROCEDURE [Reports].[Budgeting_IncomeAndSpendingForDateRange]
	@UserId UNIQUEIDENTIFIER,
	@StartDate DATE,
	@EndDate DATE
AS
BEGIN
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
     
	DECLARE @TempBudget AS TABLE(
		DateKey NVARCHAR(8),
		DisplayDate NVARCHAR(8),
		ActualIncome DECIMAL(18,2),
		ActualSpending DECIMAL(18,2)) 

	DECLARE @StartYearMoth NVARCHAR(6)=(CAST(YEAR(@StartDate) AS NCHAR(4)) + RIGHT('00' + LTRIM(MONTH(@StartDate)),2)) 
	DECLARE @EndYearMonth NVARCHAR(6)=(CAST(YEAR(@EndDate) AS NCHAR(4)) + RIGHT('00' + LTRIM(MONTH(@EndDate)),2)) 

	--capture records by given userid and insert into temp table
	INSERT INTO @TempBudget
	SELECT 
		CAST([YEAR] AS NVARCHAR)+ RIGHT('00' + LTRIM([MONTH]),2),
		RIGHT('00' + LTRIM([MONTH]),2) +'/' +CAST([YEAR] AS NVARCHAR),
		ActualIncome,
		ActualSpending
	FROM [freebyTrack].[Budgets] B
	WHERE OwnerId=@UserId
 
	DECLARE @Subtitle AS NVARCHAR(128);
	SET @Subtitle = 'Between ' + DATENAME(m, @StartDate) + ' of ' + DATENAME(yyyy, @StartDate) + ' and ' + DATENAME(m, @EndDate) + ' of ' + DATENAME(yyyy, @EndDate)
 
	DECLARE @SubTitlePart nvarchar(50)
	
	(
	SELECT 'Total Budget Income and Spending by Month' as Title, @SubTitle as Subtitle, 0 as DisplayIndex
	UNION
	SELECT 'Surplus or Deficit by Month' as Title, @SubTitle as Subtitle, 1 as DisplayIndex
	UNION
	SELECT 'Overall Budget Income and Spending' as Title, @SubTitle as Subtitle, 2 as DisplayIndex
	)
	ORDER BY DisplayIndex
 
	SELECT  T.DateKey, T.DisplayDate
	FROM @TempBudget T
	WHERE T.DateKey BETWEEN @StartYearMoth AND @EndYearMonth 
	ORDER BY T.DateKey
	 
	SELECT
		TB.DateKey,
		ActualIncome,
		ActualSpending
	FROM @TempBudget TB
	WHERE TB.DateKey BETWEEN @StartYearMoth AND @EndYearMonth 
	
END


