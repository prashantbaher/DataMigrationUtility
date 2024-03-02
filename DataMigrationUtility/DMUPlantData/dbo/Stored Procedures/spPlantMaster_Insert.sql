CREATE PROCEDURE [dbo].[spPlantMaster_Insert]
	@PlantId int,
	@PlantName nvarchar(50)
AS
BEGIN
	INSERT INTO PlantMaster 
	(PlantId, PlantName) 
	Values
	(@PlantId, @PlantName)
END
