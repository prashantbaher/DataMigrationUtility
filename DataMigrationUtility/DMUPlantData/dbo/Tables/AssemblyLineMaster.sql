CREATE TABLE [dbo].[AssemblyLineMaster]
(
	[LineId] INT NOT NULL PRIMARY KEY IDENTITY, 
	[PlantId] int NOT NULL,
    [LineName] NVARCHAR(50) NULL, 
    [LineDescription] NVARCHAR(150) NULL, 
    CONSTRAINT [FK_AssemblyLineMaster_PlantMaster] FOREIGN KEY ([PlantId]) REFERENCES [PlantMaster]([PlantId])
)
