USE [FreedomStore]
GO

IF EXISTS(SELECT * FROM sys.tables WHERE SCHEMA_NAME(schema_id) LIKE 'dbo' AND name = 'Task')  
   DROP TABLE [dbo].[Task];  
GO

CREATE TABLE [dbo].[Task](
	[TaskId] [int] IDENTITY(1, 1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](100) NULL,
	[DueDate] [datetime] NOT NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Priority] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[IsActive] [bit] NOT NULL,

	CONSTRAINT PK_Task_TaskId PRIMARY KEY ([TaskId])
) ON [PRIMARY]
GO