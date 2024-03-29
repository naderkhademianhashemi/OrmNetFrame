USE [FG_DB-light]
GO
/****** Object:  View [dbo].[V_Questions]    Script Date: 2/26/2023 12:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[V_Questions]
AS
SELECT     dbo.Questions.Question_ID AS QID,
dbo.Questions.Question_Index,
 dbo.Questions.Question_Index AS QueNum,
 dbo.Questions.Description, 
dbo.Questions.Description AS Que,
dbo.Questions.Question_Optional,
 dbo.Questions.Question_Optional AS Status, 
 dbo.Questions.Question_Type ,
                      dbo.Questions.Question_Type AS Type,
					   dbo.Questions.Form_ID,
					    dbo.Questions.Template_Type, 
					   dbo.Question_Type.Description AS Type_Desc,
					   Questions.QueIsActive
FROM         dbo.Questions INNER JOIN
                      dbo.Question_Type ON dbo.Questions.Question_Type = dbo.Question_Type.Question_Type 

GO
