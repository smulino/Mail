﻿using Mail.ScriptRunner.Helpers;
using System;
using System.IO;

namespace Mail.ScriptRunner
{
	class Program
	{
		static void Main(string[] args)
		{
			IDbHelper dbHelper = new DbHelper("Mail");

			Console.WriteLine("Commands: run | updates");

			while (true)
			{
				 Console.Write(">");

				var scriptsDirectory = GetSolutionDirectory("Scripts");
				var scripts = scriptsDirectory.GetFiles();

				string command = Console.ReadLine();

				switch (command.ToLower())
				{
					case "run":
						{
							dbHelper.CreateDatabaseIfNotExists();
							dbHelper.ExecutSqlQuery(scripts, Console.WriteLine);
						}
						break;
					case "updates":
						{
							if (!dbHelper.CkeckIfDatabaseExists())
							{
								Console.WriteLine("Database doesn't exist. Try 'run' to create database");
								continue;
							}

							dbHelper.CheckUpdates(scripts, Console.WriteLine);
						}
						break;
					default:
						{
							Console.WriteLine("Invalid command");
						}
						break;

				}
			}
		}

		static DirectoryInfo GetSolutionDirectory(string directoryName)
		{
			var rootDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent;

			return new DirectoryInfo(Path.Combine(rootDirectory.FullName, directoryName));
		}
	}
}
