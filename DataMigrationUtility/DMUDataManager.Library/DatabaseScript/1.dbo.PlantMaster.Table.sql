USE [{0}]
 
/****** Object:  Table [dbo].[PlantMaster]    Script Date: 13-02-2024 22:57:19 ******/
SET ANSI_NULLS ON
 
SET QUOTED_IDENTIFIER ON
 
CREATE TABLE [dbo].[PlantMaster](
	[PlantId] [int] NOT NULL,
	[PlantName] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PlantId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
 
