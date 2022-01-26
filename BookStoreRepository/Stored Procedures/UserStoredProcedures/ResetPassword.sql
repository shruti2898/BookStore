-- =========================================================
-- Author:	Shruti Sablaniya
-- Create date: 20.01.2022
-- Description:	Stored Procedure for User Reset Password
-- =========================================================
CREATE PROCEDURE spResetPassword
(
   @UserEmail VARCHAR(50),
   @UserPassword VARCHAR(20)
)
AS
BEGIN
    UPDATE  Users SET UserPassword = @UserPassword
    WHERE UserEmail =@UserEmail 
END