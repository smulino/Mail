begin transaction 

	insert into [dbo].[Roles] values
		('Member'),
		('Manager')

	--update ScriptHistory
	insert into [dbo].[ScriptHistory] values ('5_FillOut_Roles_Table.sql', getutcdate())

commit transaction