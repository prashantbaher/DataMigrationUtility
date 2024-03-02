
CREATE PROCEDURE [dbo].[spPlantMaster_Insert]
	@PlantId int,
	@PlantName nvarchar(50)
AS
BEGIN
	 IF NOT EXISTS(SELECT * FROM PlantMaster Where PlantId = @PlantId)
	 BEGIN
		INSERT INTO PlantMaster 
		(PlantId, PlantName) 
		Values
		(@PlantId, @PlantName)
	END
 END
