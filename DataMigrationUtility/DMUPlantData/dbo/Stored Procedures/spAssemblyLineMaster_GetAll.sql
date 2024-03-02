CREATE PROCEDURE [dbo].[spAssemblyLineMaster_GetAll]
	@PlantId int
AS
BEGIN
	SET NOCOUNT ON;

	Select * FROM [dbo].[AssemblyLineMaster]
	WHERE [PlantId] = @PlantId
END
