
ALTER TABLE dbo.Group_Permissions ADD
	AddQuestion bit NULL,
	ReadOtherAnswers bit NULL
GO
ALTER TABLE dbo.Group_Permissions ADD CONSTRAINT
	DF_Group_Permissions_AddQuestion DEFAULT 0 FOR AddQuestion
GO
ALTER TABLE dbo.Group_Permissions ADD CONSTRAINT
	DF_Group_Permissions_ReadOtherAnswers DEFAULT 0 FOR ReadOtherAnswers
GO

