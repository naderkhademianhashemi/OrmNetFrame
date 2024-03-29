USE [FG_DB-light]
GO
/****** Object:  StoredProcedure [dbo].[USP_Get_Text]    Script Date: 2/26/2023 1:19:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_Get_Text]
	-- Add the parameters for the stored procedure here
	@Instance_ID int , 
	@Question_ID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT     Text_Value, Question_ID, Instance_ID
	FROM         Text_value
	WHERE     (Question_ID = 14) AND (Instance_ID = @Instance_ID) 
END


GO
