
ALTER TABLE dbo.Form_Instance ADD
	Reported bit NULL
GO
ALTER TABLE dbo.Form_Instance ADD CONSTRAINT
	DF_Form_Instance_Reported DEFAULT 0 FOR Reported
GO
ALTER TABLE dbo.Form_Instance SET (LOCK_ESCALATION = TABLE)

