USE [{0}]
 
/****** Object:  Table [dbo].[AssemblyLineMaster]    Script Date: 13-02-2024 22:57:19 ******/
SET ANSI_NULLS ON
 
SET QUOTED_IDENTIFIER ON
 
CREATE TABLE [dbo].[AssemblyLineMaster](
	[LineId] [int] IDENTITY(1,1) NOT NULL,
	[PlantId] [int] NOT NULL,
	[LineName] [nvarchar](50) NULL,
	[LineDescription] [nvarchar](150) NULL,
PRIMARY KEY CLUSTERED 
(
	[LineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
 
ALTER TABLE [dbo].[AssemblyLineMaster]  WITH CHECK ADD  CONSTRAINT [FK_AssemblyLineMaster_PlantMaster] FOREIGN KEY([PlantId])
REFERENCES [dbo].[PlantMaster] ([PlantId])
 
ALTER TABLE [dbo].[AssemblyLineMaster] CHECK CONSTRAINT [FK_AssemblyLineMaster_PlantMaster]
 
