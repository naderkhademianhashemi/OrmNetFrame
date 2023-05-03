USE [FG_DB-light]
GO
/****** Object:  UserDefinedFunction [dbo].[FN_Questions]    Script Date: 2/26/2023 1:15:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[FN_Questions]
(
	@Question_Type smallint
)
RETURNS TABLE 
AS
RETURN 
(
	select * from [dbo].[Questions]
	where Question_Type=@Question_Type
)


GO
