-- =============================================
-- Author:		James Eby
-- Create date: 4-10-2014
-- Description:	Returns the balance of an account
--				category up to and including the
--				given date.
-- =============================================
CREATE FUNCTION [freebyTrack].[Accounting_GetCategoryBalance]
(
	@AccountCategoryID uniqueidentifier,
	@EndingDate date
)
RETURNS decimal(18, 2)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @sumOfDeposits AS decimal(18, 2)
	DECLARE @sumOfWithdrawls AS decimal(18, 2)

	SELECT @sumOfDeposits = ISNULL(SUM(aa.Amount), 0)
	FROM freebyTrack.AccountActuals aa
	INNER JOIN freebyTrack.AccountActualTemplates aat ON aa.ActualTemplateID = aat.ID
	WHERE aa.CategoryID = @AccountCategoryID AND aa.RelevantOn <= @EndingDate AND aat.IsDeposit = 1
	
	SELECT @sumOfWithdrawls = ISNULL(SUM(aa.Amount), 0)
	FROM freebyTrack.AccountActuals aa
	INNER JOIN freebyTrack.AccountActualTemplates aat ON aa.ActualTemplateID = aat.ID
	WHERE aa.CategoryID = @AccountCategoryID AND aa.RelevantOn <= @EndingDate AND aat.IsDeposit = 0
	
	RETURN @sumOfDeposits - @sumOfWithdrawls
END


