-- =============================================
-- Author:		James Eby
-- Create date: 8-31-2014
-- Description:	Retrieves the current balance for
--              all account categories for
--				an individual account up to and
--				including the given date.
-- =============================================
CREATE PROCEDURE [freebyTrack].[Accounting_GetCurrentBalanceForAccountCategories]
	@OwnerID uniqueidentifier,
	@CurrentValueDate date,
	@AccountID uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
  
    SELECT ac.ID, freebyTrack.Accounting_GetCategoryBalance(ac.ID, @CurrentValueDate) as CurrentBalance
    FROM freebyTrack.Accounts a
    INNER JOIN freebyTrack.AccountCategories ac ON a.ID = ac.AccountID
    WHERE a.OwnerID = @OwnerID AND (@AccountID = a.ID)
    ORDER BY a.DisplayIndex, ac.DisplayIndex
END


