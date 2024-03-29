USE [FG_DB-light]
GO
/****** Object:  View [dbo].[V_Form]    Script Date: 2/26/2023 12:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[V_Form]
AS
SELECT DISTINCT dbo.Forms.Form_ID, dbo.Forms.Form_Name
FROM         dbo.Forms INNER JOIN
                      dbo.Questions ON dbo.Forms.Form_ID = dbo.Questions.Form_ID
WHERE     (dbo.Questions.Question_Type = 2) OR
                      (dbo.Questions.Question_Type = 3) OR
                      (dbo.Questions.Question_Type = 4)


GO
