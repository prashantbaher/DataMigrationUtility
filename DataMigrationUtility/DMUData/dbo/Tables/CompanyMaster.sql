CREATE TABLE [dbo].[CompanyMaster]
(
	[CompanyID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CompanyName] NVARCHAR(150) NOT NULL, 
    [CompanyCode] NVARCHAR(10) NOT NULL, 
    [CompanyDescription] NVARCHAR(500) NULL
)
