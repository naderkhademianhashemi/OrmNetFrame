USE [FG_DB-light]
GO
/****** Object:  StoredProcedure [dbo].[USP_insert_parameter]    Script Date: 2/26/2023 1:19:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[USP_insert_parameter]
	@parameter_name nvarchar , 
	@parameter_des nvarchar ,
	@time_sensitive bit , 
	@loc_sentive bit , 
	@sql_query nvarchar
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

set transaction isolation level SERIALIZABLE
begin transaction parameter_tran
	INSERT INTO [Parameter]
           ([Prameter_Name]
           ,[Description]
           ,[Is_Time_Sensitive]
           ,[Is_Loc_Sensitive]
           ,[Sql_Query])
     VALUES
           (@parameter_name , @parameter_des , @time_sensitive , @loc_sentive ,@sql_query)
if @@Error<>0 begin rollback transaction return end 
DECLARE @local int
Set @local = (SELECT IDENT_CURRENT('Parameter')) 
if @@Error<>0 begin rollback transaction return end 

INSERT INTO [Parameter_State] ([Parameter_ID],[State_ID]) VALUES  (1,2)
if @@Error<>0 begin rollback transaction return end 
INSERT INTO [Parameter_Dep] ([Parameter_ID],[Department_ID]) VALUES (1,1)
if @@Error<>0 begin rollback transaction return end 

commit transaction parameter_tran

END


GO
