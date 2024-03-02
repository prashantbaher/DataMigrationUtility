CREATE PROCEDURE [dbo].[spCompany_GetAll]	
AS
BEGIN
    SET NOCOUNT ON;

	SELECT  [CompanyID],
			[CompanyName],
			[CompanyCode],
			[CompanyDescription]
	From [dbo].[CompanyMaster];

END