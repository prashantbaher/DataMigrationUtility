CREATE PROCEDURE [dbo].[spPlantMaster_AddEdit]
	@PlantId int output,
	@CompanyID int,
	@PlantName nvarchar(50),
	@PlantCode nvarchar(10),
	@PlantLocation nvarchar(150),
	@DBName NVARCHAR(50)
AS
BEGIN

	SET NOCOUNT ON;

	IF @PlantId > 0 
		BEGIN

			UPDATE PlantMaster
			SET PlantName =  @PlantName,
				PlantLocation=@PlantLocation
			WHERE PlantId =@PlantId;

		END
	ELSE 
		BEGIN 

			INSERT INTO PlantMaster
			(CompanyId, PlantName, PlantCode, PlantLocation, DBName)
			VALUES
			(@CompanyID, @PlantName,@PlantCode,@PlantLocation, @DBName);
			
			SET @PlantId=@@IDENTITY;
		END
END 