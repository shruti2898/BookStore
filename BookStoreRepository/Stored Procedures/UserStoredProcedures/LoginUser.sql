-- =========================================================
-- Author:	Shruti Sablaniya
-- Create date: 20.01.2022
-- Description:	Stored Procedure for User Login
-- =========================================================
ALTER PROCEDURE spLoginUser
(
   @UserEmail VARCHAR(50),
   @UserPassword VARCHAR(20)
)
AS
BEGIN
	SELECT UserId FROM Users WHERE UserEmail = @UserEmail and  UserPassword=@UserPassword 
END


