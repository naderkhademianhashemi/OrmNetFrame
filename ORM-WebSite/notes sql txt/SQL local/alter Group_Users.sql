
ALTER TABLE dbo.Group_Users ADD
	IsAdmin bit NOT NULL CONSTRAINT DF_Group_Users_IsAdmin DEFAULT 0
GO
