CREATE PROCEDURE [dbo].[spOperationItem_UpdateProcessStatus]
	@Id int,
	@IsProcessed bit
AS
BEGIN
	UPDATE [dbo].[OperationItem]
	SET IsProcessed = @IsProcessed
	WHERE [Id] = @Id;
END
