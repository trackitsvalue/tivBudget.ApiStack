-- =============================================
-- Author:		James Eby
-- Create date: 10-27-2015
-- Description:	Retrieves all budget actuals from 
-- all budgets for a given month / year.
-- =============================================
CREATE PROCEDURE [freebyTrack].[Budgeting_GetActualsByRelevantOnForDateRange]
	@OwnerID uniqueidentifier,
	@RelevantStartDate date,
	@RelevantEndDate date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
  
    SELECT ba.ID, ba.Description, ba.Amount, ba.RelevantOn, ba.IsEnvelopeDeposit, bct.Icon, bct.IsIncomeCategory as IsDeposit,
		bi.ID as ItemID, bi.Description as ItemDescription, bi.AmountBudgeted as ItemAmountBudgeted, 
		bc.ID as CategoryID, bc.Description as CategoryDescription, 
		b.ID as BudgetID, b.Description as BudgetDescription, b.Month as BudgetMonth, b.Year as BudgetYear
    FROM freebyTrack.BudgetActuals ba
    INNER JOIN freebyTrack.BudgetItems bi ON ba.ItemID = bi.ID
    INNER JOIN freebyTrack.BudgetCategories bc ON bi.CategoryID = bc.ID
    INNER JOIN freebyTrack.BudgetCategoryTemplates bct ON bc.CategoryTemplateID = bct.ID
    INNER JOIN freebyTrack.Budgets b ON bc.BudgetID = b.ID
    WHERE b.OwnerID = @OwnerID and ba.RelevantOn BETWEEN @RelevantStartDate AND @RelevantEndDate
    ORDER BY ba.RelevantOn
END


