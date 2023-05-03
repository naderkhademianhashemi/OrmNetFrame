with A as(
SELECT dbo.Forms.Form_ID, 
dbo.Questions.Question_ID AS QID,
 dbo.Questions.Question_Optional, 
dbo.Questions.Question_Type AS Type, 
dbo.Questions.Question_Index 
FROM   dbo.Questions,  
                 dbo.Forms  where dbo.Forms.Form_ID = dbo.Questions.Form_ID 
				 and Forms.Form_ID=65
),B as(select A.*,B.* from Question_Relation inner join A 
on A.QID=Question_Relation.QID
)

select * from B


				

				 

				   