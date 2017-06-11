using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace Mail.ScriptRunner.Helpers
{
	public class DbHelper : IDbHelper
	{
		private readonly string _databaseName;
		private readonly SqlConnection _connection;

		private const string MasterDatabaseName = "master";

		public DbHelper(string databaseName)
		{
			_databaseName = databaseName;
			_connection = new SqlConnection(string.Format(
				ConfigurationManager.ConnectionStrings["Default"].ConnectionString, databaseName));
		}

		public bool CkeckIfDatabaseExists()
		{
			var masterConnection = new SqlConnection(string.Format(
				ConfigurationManager.ConnectionStrings["Default"].ConnectionString, MasterDatabaseName));

			using (masterConnection)
			{
				masterConnection.Open();

				var sqlQueryStr = string.Format(@"
					SELECT COUNT(*) FROM [{0}].[sys].[databases] WHERE name='{1}'", 
					MasterDatabaseName, _databaseName);

				var cmd = new SqlCommand(sqlQueryStr, masterConnection);

				return (int)cmd.ExecuteScalar() > 0;
			}
		}

		public void CreateDatabase()
		{
			var masterConnection = new SqlConnection(string.Format(
				ConfigurationManager.ConnectionStrings["Default"].ConnectionString, MasterDatabaseName));

			using (masterConnection)
			{
				masterConnection.Open();

				var sqlQueryStr = string.Format(@"CREATE DATABASE {0}", _databaseName);
				var cmd = new SqlCommand(sqlQueryStr, masterConnection);
				
				cmd.ExecuteNonQuery();
			}

			CreateSriptHistoryTable();
		}

		public void CreateDatabaseIfNotExists()
		{
			if (!CkeckIfDatabaseExists())
			{
				CreateDatabase();
			}
		}

		public IEnumerable<string> GetScriptHistory()
		{
			var scriptHistory = new List<string>();
			
			using (_connection)
			{
				_connection.Open();

				var sqlQueryStr = @"
					SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES  
					WHERE TABLE_NAME = N'ScriptHistory'";

				var cmd = new SqlCommand(sqlQueryStr, _connection);

				if ((int)cmd.ExecuteScalar() > 0)
				{
					sqlQueryStr = "SELECT ScriptId FROM ScriptHistory";

					cmd = new SqlCommand(sqlQueryStr, _connection);

					var reader = cmd.ExecuteReader();

					while (reader.Read())
					{
						scriptHistory.Add(reader["ScriptId"].ToString());
					}
				}
			}

			return scriptHistory;
		}

		public void ExecutSqlQuery(FileInfo[] files, Action<string> logger)
		{
			var scriptHistory = GetScriptHistory();
			
			using (_connection)
			{
				_connection.Open();

				foreach (var file in files)
				{
					if (file.Extension == ".sql" && !scriptHistory.Contains(file.Name))
					{
						var sqlQueryStr = File.ReadAllText(file.FullName);

						var cmd = new SqlCommand(sqlQueryStr, _connection);

						cmd.ExecuteNonQuery();

						logger(file.Name);
					}
				}
			}
		}

		public void CheckUpdates(FileInfo[] files, Action<string> logger)
		{
			var scriptHistory = GetScriptHistory();

			foreach (var file in files)
			{
				if (file.Extension == ".sql" && !scriptHistory.Contains(file.Name))
				{
					logger(file.Name);
				}
			}
		}

		private void CreateSriptHistoryTable()
		{
			using (_connection)
			{
				_connection.Open();

				var sqlQueryStr = string.Format(@"
					CREATE TABLE [dbo].[ScriptHistory] (
						[ScriptId] [nvarchar] (255) NOT NULL,
						[ExecutionTimeUTC] [datetime] NOT NULL
						CONSTRAINT [PK_ScriptHistory] PRIMARY KEY CLUSTERED ([ScriptId])
					);"
				);

				var cmd = new SqlCommand(sqlQueryStr);

				cmd.ExecuteNonQuery();
			}
		}
	}
}
