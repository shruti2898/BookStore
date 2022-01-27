-- =========================================================
-- Author:	Shruti Sablaniya
-- Create date: 20.01.2022
-- Description:	Stored Procedure for Add Book
-- =========================================================
CREATE PROCEDURE spAddBook    
(   
	@Title VARCHAR(50),
	@Author VARCHAR(50),
	@Description VARCHAR(250),
	@Image VARCHAR(100),
	@Quantity int,
	@Price float,
	@DiscountPrice float,
	@Rating float,
	@RatingCount int,
	@BookId int out
)   
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF NOT EXISTS 
    (
      SELECT *
      FROM Books
      WHERE Title = @Title or Author = @Author
    )
	INSERT INTO Books(Title,Author,Description,Image,Quantity,Price,DiscountPrice,Rating,RatingCount)   
    Values (@Title,@Author,@Description,@Image,@Quantity,@Price,@DiscountPrice,@Rating,@RatingCount)   
	SET @BookId = SCOPE_IDENTITY()
	RETURN @BookId
END

