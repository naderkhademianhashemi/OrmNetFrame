USE [FG_DB-light]
GO
/****** Object:  View [dbo].[V_Personel]    Script Date: 2/26/2023 12:53:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



create VIEW [dbo].[V_Personel]

AS
SELECT   User_Name as FullName,
'1' as ShomarehPersenely,
User_Name as UserName,
User_Name,
'1' as Semat,

 IsAdmin,
Group_ID
FROM       [dbo].[Group_Users]  
                    
					 
                
            
            
            





GO
