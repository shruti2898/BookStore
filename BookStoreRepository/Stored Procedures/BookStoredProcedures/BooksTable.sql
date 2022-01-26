-- =========================================================
-- Author:	Shruti Sablaniya
-- Create date: 20.01.2022
-- Description:	Book table for BookStore Database
-- =========================================================
USE [BookStore]

CREATE TABLE [dbo].[Books](
	[BookId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NULL,
	[Author] [varchar](50) NULL,
	[Description] [varchar](250) NULL,
	[Image] [varchar](100) NULL,
	[Quantity] [int] NULL,
	[Price] [float] NULL,
	[DiscountPrice] [float] NULL,
	[Rating] [float] NULL,
	[RatingCount] [int] NULL
)

SELECT * FROM Books;