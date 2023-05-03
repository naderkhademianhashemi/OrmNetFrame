
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
						dbo.Question_Relation.Question1,
					  dbo.Question_Relation.Question1 AS QID1, 
                      dbo.Question_Relation.Condition,
					   Questions_1.Description AS Que1,
					    Questions_1.Question_Index AS QueNum1,
					   dbo.Question_Type.Description AS Type_Desc, 
                      dbo.Question_Relation.ID AS QRelationID,
					  dbo.Question_Relation.Question2
FROM         dbo.Questions INNER JOIN
                      dbo.Question_Type ON dbo.Questions.Question_Type = dbo.Question_Type.Question_Type LEFT OUTER JOIN
                      dbo.Question_Relation ON dbo.Questions.Question_ID = dbo.Question_Relation.Question2 LEFT OUTER JOIN
                      dbo.Questions AS Questions_1 ON dbo.Question_Relation.Question1 = Questions_1.Question_ID
GO

