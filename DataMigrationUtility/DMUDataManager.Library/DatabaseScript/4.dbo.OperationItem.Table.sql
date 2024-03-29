USE [{0}]
 
/****** Object:  Table [dbo].[OperationItem]    Script Date: 13-02-2024 22:57:19 ******/
SET ANSI_NULLS ON
 
SET QUOTED_IDENTIFIER ON
 
CREATE TABLE [dbo].[OperationItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OperationId] [int] NOT NULL,
	[Z1] [nvarchar](50) NULL,
	[Z2] [nvarchar](50) NULL,
	[Z3] [nvarchar](50) NULL,
	[ZEDrawingNumber] [nvarchar](50) NULL,
	[FileName] [nvarchar](50) NULL,
	[Symbol] [nvarchar](50) NULL,
	[AMT] [int] NULL,
	[SheetNumber] [nvarchar](50) NULL,
	[StockName] [nvarchar](500) NULL,
	[SupplierName] [nvarchar](500) NULL,
	[SupplierPartNumber] [nvarchar](500) NULL,
	[ItemNumber] [nvarchar](500) NULL,
	[ReferanceZENumber] [nvarchar](50) NULL,
	[ReferanceSymbol] [nvarchar](50) NULL,
	[ReferanceSheetNumber] [nvarchar](50) NULL,
	[FileIdentificationType] [int] NULL,
	[HEXNumber] [nvarchar](10) NULL,
	[NewSymbol] [nvarchar](50) NULL,
	[NewSheetNumber] [nvarchar](50) NULL,
	[RowIndex] [int] NULL,
	[Column1] [nvarchar](50) NULL,
	[Column2] [nvarchar](50) NULL,
	[Column3] [nvarchar](50) NULL,
	[IsProcessed] [bit] NULL,
	[Status1] [bit] NULL,
	[Status2] [bit] NULL,
	[Status3] [bit] NULL,
	[Status4] [bit] NULL,
	[Status5] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
 
ALTER TABLE [dbo].[OperationItem] ADD  DEFAULT ((0)) FOR [IsProcessed]
ALTER TABLE [dbo].[OperationItem] ADD  DEFAULT ((0)) FOR [Status1]
ALTER TABLE [dbo].[OperationItem] ADD  DEFAULT ((0)) FOR [Status2]
ALTER TABLE [dbo].[OperationItem] ADD  DEFAULT ((0)) FOR [Status3]
ALTER TABLE [dbo].[OperationItem] ADD  DEFAULT ((0)) FOR [Status4]
ALTER TABLE [dbo].[OperationItem] ADD  DEFAULT ((0)) FOR [Status5]
 
ALTER TABLE [dbo].[OperationItem]  WITH CHECK ADD  CONSTRAINT [FK_OperationItem_LineOperation] FOREIGN KEY([OperationId])
REFERENCES [dbo].[LineOperationMaster] ([OperationId])
 
ALTER TABLE [dbo].[OperationItem] CHECK CONSTRAINT [FK_OperationItem_LineOperation]
 
