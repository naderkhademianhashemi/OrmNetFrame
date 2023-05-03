USE [FG_DB-light]
GO
/****** Object:  UserDefinedFunction [dbo].[FN_Users]    Script Date: 2/26/2023 1:15:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[FN_Users]
(	
	@User_Name nvarchar(100)
)
RETURNS TABLE 
AS
RETURN 
(
	select * from [dbo].[Group_Users]
	where User_Name=@User_Name
)


GO
