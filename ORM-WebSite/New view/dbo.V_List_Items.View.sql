USE [FG_DB-light]
GO
/****** Object:  View [dbo].[V_List_Items]    Script Date: 3/8/2023 12:12:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



create VIEW [dbo].[V_List_Items]

AS
SELECT  [Item_ID]
      ,[Item_Text]
      ,[List_ID]
	  ,[Item_Text] as 'شرح گزینه'
      ,[List_ID] as 'شماره سوال'
  FROM [List_Items]
               


GO
