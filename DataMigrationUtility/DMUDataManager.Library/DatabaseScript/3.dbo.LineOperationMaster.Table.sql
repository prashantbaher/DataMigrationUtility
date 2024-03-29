USE [{0}]
 
/****** Object:  Table [dbo].[LineOperationMaster]    Script Date: 13-02-2024 22:57:19 ******/
SET ANSI_NULLS ON
 
SET QUOTED_IDENTIFIER ON
 
CREATE TABLE [dbo].[LineOperationMaster](
	[OperationId] [int] IDENTITY(1,1) NOT NULL,
	[AssemblyLineId] [int] NOT NULL,
	[OperationName] [nvarchar](50) NULL,
	[OperationCode] [nvarchar](50) NULL,
	[InputLocation] NVARCHAR(150) NULL, 
    [OutputLocation] NVARCHAR(150) NULL,
PRIMARY KEY CLUSTERED 
(
	[OperationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
 
ALTER TABLE [dbo].[LineOperationMaster]  WITH CHECK ADD  CONSTRAINT [FK_LineOperationMaster_AssemblyLineMaster] FOREIGN KEY([AssemblyLineId])
REFERENCES [dbo].[AssemblyLineMaster] ([LineId])
 
ALTER TABLE [dbo].[LineOperationMaster] CHECK CONSTRAINT [FK_LineOperationMaster_AssemblyLineMaster]
 
