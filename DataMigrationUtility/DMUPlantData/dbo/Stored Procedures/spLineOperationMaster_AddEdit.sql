CREATE PROCEDURE [dbo].[spLineOperationMaster_AddEdit]
	@OperationId INT OUT, 
    @AssemblyLineId INT,
    @OperationName NVARCHAR(50), 
    @OperationCode NVARCHAR(50),
    @InputLocation NVARCHAR(150),
    @OutputLocation NVARCHAR(150)
AS
BEGIN
    IF @OperationId =0 
    BEGIN
        INSERT INTO [dbo].[LineOperationMaster] 
        ([AssemblyLineId], [OperationName], [OperationCode], [InputLocation], [OutputLocation])
        VALUES
        (@AssemblyLineId, @OperationName, @OperationCode, @InputLocation, @OutputLocation);

        SET @OperationId =@@IDENTITY;
    END
    ELSE
    BEGIN
        UPDATE [dbo].[LineOperationMaster]
        SET OperationName = @OperationName,
            OperationCode = @OperationCode,
            InputLocation = @InputLocation,
            OutputLocation = @OutputLocation
        WHERE OperationId = @OperationId;
    END	
END
