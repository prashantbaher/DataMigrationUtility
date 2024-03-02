CREATE PROCEDURE [dbo].[spLineOperationMaster_GetAll]
	@LineId INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * FROM [dbo].[LineOperationMaster]
	WHERE [AssemblyLineId] = @LineId;

END
