CREATE PROCEDURE [dbo].[spOperationItem_GetByTypeAndItemNo]
	@FileIdentificationType int,
	@ItemNumber nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 1 * from [dbo].[OperationItem] 
	WHERE (ReferanceZENumber is NULL OR ReferanceZENumber ='')
		  AND FileIdentificationType = @FileIdentificationType
		  AND ItemNumber = @ItemNumber
END
