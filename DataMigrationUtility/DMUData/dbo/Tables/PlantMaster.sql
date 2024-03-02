CREATE TABLE [dbo].[PlantMaster]
(
	[PlantId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CompanyId] INT NOT NULL, 
    [PlantName] NVARCHAR(50) NOT NULL, 
    [PlantCode] NVARCHAR(10) NOT NULL, 
    [PlantLocation] NVARCHAR(150) NULL, 
    [DBName] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_PlantMaster_CompanyMaster] FOREIGN KEY ([CompanyId]) REFERENCES [CompanyMaster]([CompanyId])
)
