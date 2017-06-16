begin transaction 

	insert into [dbo].[Roles] values
		('Member'),
		('Manager')

	--update ScriptHistory
	insert into [dbo].[ScriptHistory] values ('5_Insert_Role_Records.sql', getutcdate())

commit transaction