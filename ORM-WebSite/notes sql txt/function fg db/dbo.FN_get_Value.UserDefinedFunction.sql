USE [FG_DB-light]
GO
/****** Object:  UserDefinedFunction [dbo].[FN_get_Value]    Script Date: 2/26/2023 1:15:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[FN_get_Value] 
(	
	-- Add the parameters for the function here
	@Param_ID as bigint
)
RETURNS TABLE 
AS
RETURN 
(
	-- Add the SELECT statement with parameter references here
	select sql_query from parameter where Parameter_ID = @Param_ID
)


GO
