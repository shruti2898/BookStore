-- =========================================================
-- Author:	Shruti Sablaniya
-- Create date: 20.01.2022
-- Description:	Address table for BookStore Database
-- =========================================================
USE [BookStore]

CREATE TABLE [dbo].[Address](
    [AddressId] [int] IDENTITY(1,1) PRIMARY KEY,
	[UserId] int FOREIGN KEY REFERENCES Users(UserId),
	[AddressType] [varchar](20) NOT NULL,
	[FullAddress] [varchar](255) NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](50) NULL
)

SELECT * FROM Address;
