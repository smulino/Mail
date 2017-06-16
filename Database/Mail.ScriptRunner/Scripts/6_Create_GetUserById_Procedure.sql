begin transaction 

	SET QUOTED_IDENTIFIER OFF 
	GO
	SET ANSI_NULLS OFF 
	GO

	if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetUserById]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
		drop procedure [dbo].[GetUserById]
	GO

	CREATE PROCEDURE [dbo].[GetUserById] @userId int
	AS
	begin
		select top(1) * from [dbo].[Users] u where u.Id = @userId 
	end

	GO
	SET QUOTED_IDENTIFIER OFF 
	GO
	SET ANSI_NULLS ON 
	GO

	--update ScriptHistory
	insert into [dbo].[ScriptHistory] values ('6_Create_GetUserById_Procedure.sql', getutcdate())

commit transaction