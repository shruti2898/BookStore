-- =========================================================
-- Author:	Shruti Sablaniya
-- Create date: 20.01.2022
-- Description:	Stored Procedure for User Forgot Password
-- =========================================================
CREATE PROCEDURE spForgotPassword
(
   @UserEmail VARCHAR(50)
)
AS
BEGIN
	 SELECT * FROM Users WHERE UserEmail = @UserEmail
END
