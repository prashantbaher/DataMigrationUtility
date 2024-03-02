CREATE PROCEDURE [dbo].[spAssemblyLineMaster_AddEdit]
	@LineId INT OUT,
	@PlantId INT,
	@LineName NVARCHAR(50),
	@LineDescription NVARCHAR(150)
AS
BEGIN
	IF @LineId = 0 
	BEGIN
		INSERT INTO [dbo].[AssemblyLineMaster]
		(PlantId, LineName, LineDescription)
		VALUES
		(@PlantId, @LineName, @LineDescription);

		SET @LineId=@@IDENTITY;
	END
	ELSE
	BEGIN
		UPDATE [dbo].[AssemblyLineMaster]
		SET [LineName] = @LineName,
			[LineDescription] = @LineDescription
		WHERE LineId = @LineId;
	END
END
	