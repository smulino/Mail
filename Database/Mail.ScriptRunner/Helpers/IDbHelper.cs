using System;
using System.Collections.Generic;
using System.IO;

namespace Mail.ScriptRunner.Helpers
{
	public interface IDbHelper
	{
		bool CkeckIfDatabaseExists();
		void CreateDatabase();
		void CreateDatabaseIfNotExists();
		IEnumerable<string> GetScriptHistory();
		void ExecutSqlQuery(FileInfo[] files, Action<string> logger);
		void CheckUpdates(FileInfo[] files, Action<string> logger);
	}
}
