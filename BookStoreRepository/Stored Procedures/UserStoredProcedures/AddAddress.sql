-- =========================================================
-- Author:	Shruti Sablaniya
-- Create date: 27.01.2022
-- Description:	Stored Procedure for Add Address
-- =========================================================
CREATE PROCEDURE spAddAddress    
(   
    
	@UserId int,
	@AddressType VARCHAR(20),
	@FullAddress VARCHAR(255),
	@City VARCHAR(50),
	@State VARCHAR(50),
	@AddressId int out
)   
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Address(UserId,AddressType,FullAddress, City,State)   
    Values (@UserId,@AddressType,@FullAddress, @City,@State)   
	SET @AddressId = SCOPE_IDENTITY()
	RETURN @AddressId
END

