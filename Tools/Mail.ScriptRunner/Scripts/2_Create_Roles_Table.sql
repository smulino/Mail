begin transaction 

	create table Roles (
		[Id] int identity(1, 1) not null,
		[Name] nvarchar(250) not null
		constraint PK_Role primary key clustered ([Id])
	)

	--update ScriptHistory
	insert into [dbo].[ScriptHistory] values ('2_Create_Roles_Table.sql', getutcdate())

commit transaction