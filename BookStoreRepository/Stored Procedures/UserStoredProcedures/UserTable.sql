-- =========================================================
-- Author:	Shruti Sablaniya
-- Create date: 20.01.2022
-- Description:	User table for BookStore Database
-- =========================================================
USE [BookStore]

CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) PRIMARY KEY,
	[UserName] [varchar](30) NULL,
	[UserEmail] [varchar](50) NULL,
	[UserMobile] [varchar](30) NULL,
	[UserPassword] [varchar](20) NULL,
)

SELECT * FROM Users;