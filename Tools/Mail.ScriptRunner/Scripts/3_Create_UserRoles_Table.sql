begin transaction 

	create table UserRoles (
		[UserId] int not null,
		[RoleId] int not null
	)

	alter table UserRoles 
	add constraint FK_UserRole_User foreign key ([UserId]) references Users([Id]) on delete cascade

	alter table UserRoles
	add constraint FK_UserRole_Role foreign key ([RoleId]) references Roles([Id]) on delete cascade

	alter table UserRoles
	add constraint PK_UserRole primary key clustered ([UserId], [RoleId])

	--update ScriptHistory
	insert into [dbo].[ScriptHistory] values ('3_Create_UserRoles_Table.sql', getutcdate())

commit transaction