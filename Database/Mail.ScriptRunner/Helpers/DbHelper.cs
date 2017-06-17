using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
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
		private readonly string databaseName;

		private readonly string connectionString;

		private readonly static string MasterConnectionString;

		private const string MasterDatabaseName = "master";

		static DbHelper()
		{
			MasterConnectionString = string.Format(
				ConfigurationManager.ConnectionStrings["Default"].ConnectionString, MasterDatabaseName);
		}

		public DbHelper(string databaseName)
		{
			this.databaseName = databaseName;

			connectionString = string.Format(
				ConfigurationManager.ConnectionStrings["Default"].ConnectionString, databaseName);
		}

		public bool CkeckIfDatabaseExists()
		{
			using (var masterConnection = new SqlConnection(MasterConnectionString))
			{
				masterConnection.Open();

				var sqlQueryStr = string.Format(@"
					SELECT COUNT(*) FROM [{0}].[sys].[databases] WHERE name='{1}'",
					MasterDatabaseName, databaseName);

				var cmd = new SqlCommand(sqlQueryStr, masterConnection);

				return (int)cmd.ExecuteScalar() > 0;
			}
		}

		public void CreateDatabase()
		{
			using (var masterConnection = new SqlConnection(MasterConnectionString))
			{
				masterConnection.Open();

				var sqlQueryStr = string.Format(@"CREATE DATABASE {0}", databaseName);
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

			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();

				var sqlQueryStr = @"
					SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES  
					WHERE TABLE_NAME = N'ScriptHistory'";

				var cmd = new SqlCommand(sqlQueryStr, connection);

				sqlQueryStr = "SELECT ScriptId FROM ScriptHistory";

				cmd = new SqlCommand(sqlQueryStr, connection);

				var reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					scriptHistory.Add(reader["ScriptId"].ToString());
				}
			}

			return scriptHistory;
		}

		public void ExecutSqlQuery(FileInfo[] files, Action<string> logger)
		{
			var scriptHistory = GetScriptHistory();

			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();

				foreach (var file in files)
				{
					if (file.Extension == ".sql" && !scriptHistory.Contains(file.Name))
					{
						var sqlQueryStr = File.ReadAllText(file.FullName);
						
						Server server = new Server(new ServerConnection(connection));
						server.ConnectionContext.ExecuteNonQuery(sqlQueryStr);

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
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();

				var sqlQueryStr = string.Format(@"
					CREATE TABLE [dbo].[ScriptHistory] (
						[ScriptId] [nvarchar] (255) NOT NULL,
						[ExecutionTimeUTC] [datetime] NOT NULL
						CONSTRAINT [PK_ScriptHistory] PRIMARY KEY CLUSTERED ([ScriptId])
					);"
				);

				var cmd = new SqlCommand(sqlQueryStr, connection);

				cmd.ExecuteNonQuery();
			}
		}
	}
}
