
IF DB_ID('LibraryManagement') IS NOT NULL
	DROP DATABASE LibraryManagement
	
CREATE DATABASE LibraryManagement
GO

USE [LibraryManagement]
GO

/****** Create Users table ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Password] [varchar](20) NOT NULL,
	[Admin] [int] NOT NULL DEFAULT 0,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Create Books table ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Books] (
	[BookID] [int] IDENTITY(1,1) NOT NULL,
	[AuthorName] [varchar](100) NOT NULL,
	[CategoryID] [int] NOT NULL,
	[BookName] [varchar](100) NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[Owner] [int] NOT NULL,
	[ReturnDate] dateTime,
	[Lended_by] [int] ,
	[Reserved_by] [int],
 CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED 
(
	[BookID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Create Category table ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



/****** Constraints for tables ******/

ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_CategoryID] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([CategoryID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_CategoryID]
GO

ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Owner] FOREIGN KEY([Owner])
REFERENCES [dbo].[Users] ([UserID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Owner]
GO

ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Lended_by] FOREIGN KEY([Lended_by])
REFERENCES [dbo].[Users] ([UserID])
GO

ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Lended_by]
GO

ALTER TABLE [dbo].[Books]  WITH CHECK ADD  CONSTRAINT [FK_Reserved_by] FOREIGN KEY([Reserved_by])
REFERENCES [dbo].[Users] ([UserID])
GO

ALTER TABLE [dbo].[Books] CHECK CONSTRAINT [FK_Reserved_by]
GO

/***Insert values into category table**/
INSERT INTO [dbo].[Category] (CategoryName) VALUES ('Non Fiction')
INSERT INTO [dbo].[Category] (CategoryName) VALUES ('Horror')
INSERT INTO [dbo].[Category] (CategoryName) VALUES ('Poem')
INSERT INTO [dbo].[Category] (CategoryName) VALUES ('Novel')