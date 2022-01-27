-- =========================================================
-- Author:	Shruti Sablaniya
-- Create date: 27.01.2022
-- Description:	Stored Procedure for Delete Address
-- =========================================================
CREATE PROCEDURE spDeleteAddress   
(   
	@AddressId int
)   
AS
BEGIN
    DELETE FROM Address WHERE AddressId=@AddressId;
END