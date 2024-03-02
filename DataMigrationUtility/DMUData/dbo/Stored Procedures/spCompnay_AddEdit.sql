CREATE PROCEDURE [dbo].[spCompany_AddEdit]
	@CompanyID int output,
	@CompanyName nvarchar(150),
	@CompanyCode nvarchar(10),
	@CompanyDescription nvarchar(500)
AS
BEGIN

	SET NOCOUNT ON;

	IF @CompanyID > 0 
		BEGIN

			UPDATE CompanyMaster
			SET CompanyDescription =  @CompanyDescription
			WHERE CompanyID =@CompanyID;

		END
	ELSE 
		BEGIN 

			INSERT INTO CompanyMaster
			(CompanyName, CompanyCode, CompanyDescription)
			VALUES
			(@CompanyName, @CompanyCode, @CompanyDescription);
			
			SET @CompanyID=@@IDENTITY;
		END
END 