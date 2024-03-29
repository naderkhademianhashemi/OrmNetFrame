USE [FG_DB-light]
GO
/****** Object:  View [dbo].[V_Que]    Script Date: 2/26/2023 12:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[V_Que]
AS
SELECT     dbo.Questions.Question_ID,
dbo.Questions.Question_Index,
 dbo.Questions.Description, 
dbo.Questions.Question_Optional,
 dbo.Questions.Question_Type ,
					   dbo.Questions.Form_ID,
					    dbo.Questions.Template_Type, 
					   dbo.Question_Type.Description AS Que_Type_Description,
					   Questions.QueIsActive
FROM         dbo.Questions INNER JOIN
                      dbo.Question_Type ON dbo.Questions.Question_Type = dbo.Question_Type.Question_Type 


GO
