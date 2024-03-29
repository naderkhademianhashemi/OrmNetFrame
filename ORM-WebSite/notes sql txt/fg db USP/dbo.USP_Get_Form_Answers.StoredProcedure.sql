USE [FG_DB-light]
GO
/****** Object:  StoredProcedure [dbo].[USP_Get_Form_Answers]    Script Date: 2/26/2023 1:19:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================

CREATE PROCEDURE [dbo].[USP_Get_Form_Answers] (@from_id bigint)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
sELECT     Text_value.Instance_ID, Questions.Description AS Question, Text_value.Text_Value AS Answer  , Questions.Question_Type as Question_Type , Questions.Template_Type as Template_Type
FROM         dbo.Text_value INNER JOIN
                      dbo.Questions ON dbo.Text_value.Question_ID = dbo.Questions.Question_ID
WHERE     (dbo.Questions.Question_Type = 1 AND Text_value.Instance_ID in(Select Instance_id from Form_Instance where form_id = @from_id))


UNION

SELECT     dbo.Date_Value.Instance_ID, Questions_1.Description AS Question, convert(varchar , dbo.Date_Value.Date_value) AS Answer , Questions_1.Question_Type as Question_Type ,  Questions_1.Template_Type as Template_Type
FROM         dbo.Date_Value INNER JOIN
                      dbo.Questions AS Questions_1 ON dbo.Date_Value.Question_ID = Questions_1.Question_ID
WHERE     (Questions_1.Question_Type = 6 AND Instance_ID in(Select Instance_id from Form_Instance where form_id = @from_id))


union 
SELECT     dbo.Number_Value.Instance_ID, Questions_2.Description AS Question, convert(varchar ,dbo.Number_Value.Number_Value) AS Answer ,Questions_2.Question_Type as Question_Type , Questions_2.Template_Type as Template_Type
FROM         dbo.Questions AS Questions_2 INNER JOIN
                      dbo.Number_Value ON Questions_2.Question_ID = dbo.Number_Value.Question_ID
WHERE     (Questions_2.Question_Type = 5 and Instance_ID in(Select Instance_id from Form_Instance where form_id = @from_id))

union 

SELECT     dbo.Item_Choice.Form_Instance_ID AS Instance_ID, dbo.Questions.Description AS Question, dbo.List_Items.Item_Text AS Answer,Questions.Question_Type as Question_Type , Questions.Template_Type as Template_Type
FROM         dbo.Questions INNER JOIN
                      dbo.Item_Choice ON dbo.Questions.Question_ID = dbo.Item_Choice.Question_ID INNER JOIN
                      dbo.List_Items ON dbo.Item_Choice.Selected_Item = dbo.List_Items.Item_ID
WHERE     (dbo.Questions.Template_Type = 4 and Item_Choice.Form_Instance_ID in(Select Instance_id from Form_Instance where form_id = @from_id) )


union

SELECT     dbo.Item_Choice.Form_Instance_ID AS Instance_ID, dbo.Questions.Description AS Question, dbo.Department.DepName AS Answer ,Questions.Question_Type as Question_Type , Questions.Template_Type as Template_Type
FROM         dbo.Questions INNER JOIN
                      dbo.Item_Choice ON dbo.Questions.Question_ID = dbo.Item_Choice.Question_ID INNER JOIN
                      dbo.Dep_Choice AS Dep_Choice_1 ON dbo.Item_Choice.Form_Instance_ID = Dep_Choice_1.Form_Instance_ID INNER JOIN
                      dbo.Department ON Dep_Choice_1.Dep_Item = dbo.Department.DepID
WHERE     (dbo.Questions.Template_Type = 3 and Item_Choice.Form_Instance_ID in(Select Instance_id from Form_Instance where form_id = @from_id))

union 

SELECT     dbo.Item_Choice.Form_Instance_ID AS Instance_ID, dbo.Questions.Description AS Question, dbo.Branch.BranchName AS Answer ,Questions.Question_Type as Question_Type, Questions.Template_Type as Template_Type
FROM         dbo.Questions INNER JOIN
                      dbo.Item_Choice ON dbo.Questions.Question_ID = dbo.Item_Choice.Question_ID INNER JOIN
                      dbo.Branch_Choice ON dbo.Item_Choice.Form_Instance_ID = dbo.Branch_Choice.Form_Instance_ID INNER JOIN
                      dbo.Branch ON dbo.Branch_Choice.Branch_Item = dbo.Branch.BID
WHERE     (dbo.Questions.Template_Type = 2and Item_Choice.Form_Instance_ID in(Select Instance_id from Form_Instance where form_id = @from_id))
union 

SELECT     dbo.Item_Choice.Form_Instance_ID AS Instance_ID, dbo.Questions.Description AS Question, dbo.State.LocName AS Answer,Questions.Question_Type as Question_Type, Questions.Template_Type as Template_Type
FROM         dbo.Questions INNER JOIN
                      dbo.Item_Choice ON dbo.Questions.Question_ID = dbo.Item_Choice.Question_ID INNER JOIN
                      dbo.State_Choice ON dbo.Questions.Question_ID = dbo.State_Choice.Question_ID INNER JOIN
                      dbo.State ON dbo.State_Choice.State_Item = dbo.State.locid
WHERE     (dbo.Questions.Template_Type = 1and Item_Choice.Form_Instance_ID in(Select Instance_id from Form_Instance where form_id = @from_id))
END


GO
