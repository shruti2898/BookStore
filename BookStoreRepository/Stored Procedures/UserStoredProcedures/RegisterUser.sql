-- =========================================================
-- Author:	Shruti Sablaniya
-- Create date: 20.01.2022
-- Description:	Stored Procedure for User Registration
-- =========================================================
Alter PROCEDURE spRegisterUser    
(   
    @UserName VARCHAR(30),  
    @UserEmail VARCHAR(50), 
	@UserMobile VARCHAR(30),
    @UserPassword VARCHAR(20),
	@UserId int out
)   
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF NOT EXISTS 
   (
      SELECT UserEmail , UserMobile
      FROM Users
      WHERE UserEmail = @UserEmail or UserMobile = @UserMobile
   )
	INSERT INTO Users(UserName,UserEmail,UserMobile, UserPassword)   
    Values (@UserName,@UserEmail,@UserMobile, @UserPassword)   
	SET @UserId = SCOPE_IDENTITY()
	RETURN @UserId
END

