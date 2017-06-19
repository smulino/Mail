using Mail.ScriptRunner.Helpers;
using System;
using System.IO;

namespace Mail.ScriptRunner
{
	class Program
	{
		static void Main(string[] args)
		{
			using (IDbHelper dbHelper = new DbHelper("Mail"))
			{
				Console.WriteLine("Commands: run | updates");

				while (true)
				{
					Console.Write(">");

					var scripts = GetSolutionFiles("Scripts");

					string command = Console.ReadLine();

					switch (command.ToLower())
					{
						case "run":
							dbHelper.ExecutSqlQuery(scripts, Console.WriteLine);
							break;
						case "updates":
							dbHelper.CheckUpdates(scripts, Console.WriteLine);
							break;
						default:
							Console.WriteLine("Invalid command");
							break;

					}
				}
			}
		}

		static FileInfo[] GetSolutionFiles(string directoryName)
		{
			var rootDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent;
			var solutionDirectory = new DirectoryInfo(Path.Combine(rootDirectory.FullName, directoryName));

			return solutionDirectory.GetFiles();
		}
	}
}
