USE [FG_DB-light]
GO
/****** Object:  UserDefinedFunction [dbo].[FN_Get_SQL_Query]    Script Date: 2/26/2023 1:15:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[FN_Get_SQL_Query]
(
	-- Add the parameters for the function here
	@Param_ID as bigint
)
RETURNS nvarchar(max)
AS
BEGIN
	
	Declare @sqlquery nvarchar(max)
	set @sqlquery = (select sql_query from parameter where Parameter_ID = @Param_ID)
	-- Return the result of the function
	RETURN @sqlquery

END


GO
