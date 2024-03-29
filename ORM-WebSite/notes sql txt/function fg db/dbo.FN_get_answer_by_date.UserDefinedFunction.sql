USE [FG_DB-light]
GO
/****** Object:  UserDefinedFunction [dbo].[FN_get_answer_by_date]    Script Date: 2/26/2023 1:15:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[FN_get_answer_by_date]
(	
	-- Add the parameters for the function here
	@from_date datetime , @to_date datetime
)
RETURNS TABLE 
AS
RETURN 
(
	select * from V_answers where datediff(d,filldate,@from_date)<= 0 AND datediff(d,filldate,@to_date)>=0
)


GO
