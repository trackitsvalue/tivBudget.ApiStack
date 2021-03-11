-- =============================================
-- Author:		Eun Eby
-- Create date: 08-07-2014
-- Description:	Get Acount Line Items by Account ID
-- =============================================
CREATE PROCEDURE [Reports].[Accounting_ActualsForDateRange]
	@UserId UNIQUEIDENTIFIER,
	@AccountId UNIQUEIDENTIFIER,
	@StartDate DATE,
	@EndDate DATE
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;

	IF OBJECT_ID('tempdb..#AccountResults') IS NOT NULL
		DROP TABLE #AccountResults	
	
    -- Insert statements for procedure here
	SELECT 
		AAT.ID,
		AAT.[Description], -- AS TypeDescription, 
		--AA.[Description],
		CAST(DATEPART(year, AA.RelevantOn) AS NVARCHAR)+ RIGHT('00' + LTRIM(DATEPART(month, AA.RelevantOn)),2) as DateKey,
		RIGHT('00' + LTRIM(DATEPART(month, AA.RelevantOn)),2) +'/' +CAST(DATEPART(year, AA.RelevantOn) AS NVARCHAR) as DisplayDate,
		AA.RelevantOn,
		CASE AAT.IsDeposit
		WHEN 1 THEN AA.Amount
		ELSE AA.Amount * -1
		END as Amount,
		AAT.IsDeposit
	INTO #AccountResults
	FROM freebyTrack.Accounts A
	JOIN freebyTrack.AccountCategories AC
		ON AC.AccountID=A.ID
	JOIN freebyTrack.AccountActuals AA
		ON AA.CategoryID=AC.ID
	JOIN freebyTrack.AccountActualTemplates AAT
		ON AAT.ID=AA.ActualTemplateID
	WHERE A.OwnerID=@UserId
		AND A.ID=@AccountId
		AND AA.RelevantOn BETWEEN @StartDate AND @EndDate
	ORDER BY AA.RelevantOn
	
	DECLARE @Subtitle AS NVARCHAR(128);
	SET @Subtitle = 'Between ' + DATENAME(m, @StartDate) + ' of ' + DATENAME(yyyy, @StartDate) + ' and ' + DATENAME(m, @EndDate) + ' of ' + DATENAME(yyyy, @EndDate)
 
	DECLARE @totalCredits DECIMAL(18, 2), @totalDebits DECIMAL(18, 2)
 
	SELECT @totalCredits = SUM(Amount)
	FROM #AccountResults
	WHERE IsDeposit = 1
	
	SELECT @totalDebits = SUM(Amount * -1)
	FROM #AccountResults
	WHERE IsDeposit = 0
	
	(
	SELECT 'Total Account Value by Month' as Title, @SubTitle as Subtitle, 0 as DisplayIndex
	UNION
	SELECT 'Breakdown of Account Activity by Month' as Title, @SubTitle as Subtitle, 1 as DisplayIndex
	UNION
	SELECT 'Overall Account Deposits, Payments and Credits by Type' as Title, CAST(@totalCredits as NVARCHAR(18)) + ' Credited to Account ' + @SubTitle as Subtitle, 2 as DisplayIndex
	UNION
	SELECT 'Overall Account Withdrawls and Debits by Type' as Title, CAST(@totalDebits as NVARCHAR(18)) + ' Debited from Account ' + @SubTitle as Subtitle, 3 as DisplayIndex
	)
	ORDER BY DisplayIndex
	
	DECLARE @PreviousBalanceDate DATE = DATEADD(DAY, -1, @StartDate)
	
	DECLARE @PreviousBalance DECIMAL(18,2) = freebyTrack.Accounting_GetAccountBalance(@AccountId, @PreviousBalanceDate)
	
	SELECT 
		'Previous Balance' as [Description],
		@PreviousBalanceDate as RelevantOn,
		@PreviousBalance as Amount
	
	SELECT DISTINCT DateKey, DisplayDate
	FROM #AccountResults
	GROUP BY DateKey, DisplayDate
	ORDER BY DateKey
	
	SELECT 
		DateKey,
		[Description],
		SUM(Amount) as Amount,
		ID,
		IsDeposit
	FROM #AccountResults
	GROUP BY DateKey, [Description], ID, IsDeposit
	ORDER BY ID, DateKey
	
	--SELECT * FROM #AccountResults
	--ORDER BY RelevantOn
	
	IF OBJECT_ID('tempdb..#AccountResults') IS NOT NULL
		DROP TABLE #AccountResults
END


