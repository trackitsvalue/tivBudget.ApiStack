/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

:r .\Lookups\Data\Lookups.Applications.sql
:r .\Lookups\Data\Lookups.ReportCategories.sql

:r .\Applications\Data\Applications.Menus.sql

:r .\Security\Data\Security.Groups.sql
:r .\Security\Data\Security.Users.sql


:r .\PublicContent\Data\PublicContent.News.sql
:r .\PublicContent\Data\PublicContent.PageContent.sql
:r .\PublicContent\Data\PublicContent.Quotes.sql
:r .\PublicContent\Data\PublicContent.VideoCategories.sql
:r .\PublicContent\Data\PublicContent.Videos.sql

:r .\Miscellaneous\Data\Miscellaneous.Configuration.sql

:r .\Reports\Data\Reports.Reports.sql
:r .\Reports\Data\Reports.ReportControls.sql

:r .\freebyTrack\Data\freebyTrack.AccountTypes.sql
:r .\freebyTrack\Data\freebyTrack.AccountTemplates.sql
:r .\freebyTrack\Data\freebyTrack.AccountCategoryTemplates.sql
:r .\freebyTrack\Data\freebyTrack.AccountActualTemplates.sql
:r .\freebyTrack\Data\freebyTrack.BudgetCategoryTemplates.sql
:r .\freebyTrack\Data\freebyTrack.BudgetItemTemplates.sql

:r .\SeedData.sql