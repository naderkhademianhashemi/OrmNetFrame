USE [FG_DB-light]
GO
/****** Object:  UserDefinedFunction [dbo].[FN_Forms]    Script Date: 2/26/2023 1:15:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[FN_Forms]
(
	@Form_Name nvarchar(50)
)
RETURNS TABLE 
AS
RETURN 
(
	select * from [dbo].[Forms]
	where Form_Name like '%'+@Form_Name+'%'
)


GO
