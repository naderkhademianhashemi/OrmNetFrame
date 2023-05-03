USE [FG_DB]
GO

/****** Object:  View [dbo].[Answers]    Script Date: 2/18/2023 4:13:53 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

alter VIEW [dbo].[Answers]
AS
SELECT     dbo.Form_Instance.Instance_ID, dbo.Form_Instance.filldate, dbo.Form_Instance.User_name, dbo.Form_Question_Instance.Form_Instance_ID, 
                      dbo.Form_Question_Instance.Question_ID, dbo.Questions.Question_Type, dbo.Questions.Template_Type, dbo.Form_Instance.Form_ID, 
                      dbo.Form_Question_Instance.Text, dbo.Form_Question_Instance.Date, dbo.Form_Question_Instance.Number, dbo.Item_Choice.Selected_Item, 
                      dbo.Dep_Choice.Dep_Item, dbo.Branch_Choice.Branch_Item, dbo.State_Choice.State_Item, dbo.Form_Question_Instance.Answer_ID, 
                      dbo.Form_Question_Instance.Item_ID, dbo.List_Items.Item_Text, dbo.List_Items.Item_ID AS Expr1,1 AS Reported
FROM         dbo.Form_Question_Instance INNER JOIN
                      dbo.Form_Instance ON dbo.Form_Question_Instance.Form_Instance_ID = dbo.Form_Instance.Instance_ID INNER JOIN
                      dbo.Questions ON dbo.Form_Question_Instance.Question_ID = dbo.Questions.Question_ID LEFT OUTER JOIN
                      dbo.List_Items ON dbo.Form_Question_Instance.Item_ID = dbo.List_Items.Item_ID LEFT OUTER JOIN
                      dbo.State_Choice ON dbo.Form_Question_Instance.Form_Instance_ID = dbo.State_Choice.Form_Instance_Id AND 
                      dbo.Form_Question_Instance.Question_ID = dbo.State_Choice.Question_ID LEFT OUTER JOIN
                      dbo.Item_Choice ON dbo.Form_Question_Instance.Form_Instance_ID = dbo.Item_Choice.Form_Instance_ID AND 
                      dbo.Form_Question_Instance.Question_ID = dbo.Item_Choice.Question_ID LEFT OUTER JOIN
                      dbo.Dep_Choice ON dbo.Form_Question_Instance.Form_Instance_ID = dbo.Dep_Choice.Form_Instance_ID AND 
                      dbo.Form_Question_Instance.Question_ID = dbo.Dep_Choice.Question_ID LEFT OUTER JOIN
                      dbo.Branch_Choice ON dbo.Form_Question_Instance.Form_Instance_ID = dbo.Branch_Choice.Form_Instance_ID AND 
                      dbo.Form_Question_Instance.Question_ID = dbo.Branch_Choice.Question_ID
GO

