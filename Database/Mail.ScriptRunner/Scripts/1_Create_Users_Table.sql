begin transaction 

	create table [dbo].[Users] (
		[Id] int identity(1,1) not null,
		[FirstName] nvarchar (250) not null,
		[LastName] nvarchar(250) not null,
		[UserName] nvarchar(250) not null,
		[Password] nvarchar(250) not null
	)

	alter table [dbo].[Users]
	add constraint [PK_User] primary key clustered ([Id]) 

	--update ScriptHistory
	insert into [dbo].[ScriptHistory] values ('1_Create_Users_Table.sql', getutcdate())

commit transaction