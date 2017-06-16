begin transaction 

	create table [dbo].[Messages] (
		[Id] int identity(1, 1) not null,
		[Text] nvarchar(250) not null,
		[SenderUserId] int not null,
		[ReceiverUserId] int not null,
		[CreationTimeUtc] datetime not null,
		constraint PK_Message primary key clustered ([Id]), 
		constraint FK_Message_ReceiverUser foreign key ([ReceiverUserId]) references Users([Id]),
		constraint FK_Message_SenderUser foreign key ([SenderUserId]) references Users([Id])
	)

	--update ScriptHistory
	insert into [dbo].[ScriptHistory] values ('4_Create_Messages_Table.sql', getutcdate())

commit transaction