-- =========================================================
-- Author:	Shruti Sablaniya
-- Create date: 20.01.2022
-- Description:	Stored Procedure for Update Book
-- =========================================================
ALTER PROCEDURE spUpdateBook    
(   
    @BookId int,
	@Title VARCHAR(50),
	@Author VARCHAR(50),
	@Description VARCHAR(250),
	@Image VARCHAR(100) null,
	@Quantity int,
	@Price float,
	@DiscountPrice float,
	@Rating float,
	@RatingCount int
)   
AS
BEGIN
	UPDATE Books SET Title=@Title,Author=@Author,Description=@Description,Image=@Image,Quantity=@Quantity,
	Price=@Price,DiscountPrice=@DiscountPrice,Rating=@Rating,RatingCount=@RatingCount
	WHERE BookId = @BookId
END

