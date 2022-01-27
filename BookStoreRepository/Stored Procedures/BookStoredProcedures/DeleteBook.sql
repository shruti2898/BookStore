-- =========================================================
-- Author:	Shruti Sablaniya
-- Create date: 20.01.2022
-- Description:	Stored Procedure for Delete Book
-- =========================================================
CREATE PROCEDURE spDeleteBook    
(   
	@BookId int
)   
AS
BEGIN
    DELETE FROM Books WHERE BookId=@BookId;
END