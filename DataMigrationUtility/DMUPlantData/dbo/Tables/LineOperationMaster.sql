CREATE TABLE [dbo].[LineOperationMaster]
(
	[OperationId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [AssemblyLineId] INT NOT NULL,
    [OperationName] NVARCHAR(50) NULL, 
    [OperationCode] NVARCHAR(50) NULL, 
    [InputLocation] NVARCHAR(150) NULL, 
    [OutputLocation] NVARCHAR(150) NULL, 
    CONSTRAINT [FK_LineOperationMaster_AssemblyLineMaster] FOREIGN KEY ([AssemblyLineId]) REFERENCES [AssemblyLineMaster]([LineId])
)
