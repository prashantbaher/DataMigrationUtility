CREATE PROCEDURE [dbo].[spPlantMaster_GetAll]
	@CompanyId int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM [dbo].[PlantMaster] WHERE CompanyId =@CompanyId;
END
