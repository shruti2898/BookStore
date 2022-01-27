-- =========================================================
-- Author:	Shruti Sablaniya
-- Create date: 27.01.2022
-- Description:	Stored Procedure for Update Address
-- =========================================================
CREATE PROCEDURE spUpdateAddress    
(   
    @AddressId int,
	@UserId int,
	@AddressType VARCHAR(20),
	@FullAddress VARCHAR(255),
	@City VARCHAR(50),
	@State VARCHAR(50)
)   
AS
BEGIN
	UPDATE Address SET UserId=@UserId, AddressType=@AddressType,FullAddress=@FullAddress,City=@City,State=@State
	WHERE AddressId = @AddressId
END