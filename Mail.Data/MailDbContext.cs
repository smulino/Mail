using Mail.Data.Domain;
using Mail.Data.Mapping;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace Mail.Data
{
	public class MailDbContext : DbContext, IDbContext
	{
		public MailDbContext()
			: base("Default")
		{
		}

		public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
		{
			return base.Set<TEntity>();
		}
		
		public IList<TResult> ExecuteStoredProcedure<TResult>(string procedureName, Dictionary<string, object> parameters)
		{
			var sqlParameters = new List<SqlParameter>();

			string commandText = procedureName; 

			if (parameters.Any())
			{
				commandText += " " + string.Join(",", parameters.Keys.AsEnumerable());

				foreach (var parameter in parameters)
				{
					sqlParameters.Add(new SqlParameter(parameter.Key, (int)parameter.Value));
				}
			}

			var result = this.Database.SqlQuery<TResult>(commandText, sqlParameters.ToArray()).ToList();

			return result;
		}

		public TResult ExecuteStoredProcedureScalar<TResult>(string procedureName, Dictionary<string, object> parameters)
		{
			return ExecuteStoredProcedure<TResult>(procedureName, parameters).FirstOrDefault();
		}
		
		public User GetUserById(int Id)
		{
			var userId = new SqlParameter("@userId", Id);

			var user = this.Database.SqlQuery<User>("GetUserById @userId", userId).ToList().FirstOrDefault();

			return user;
		}
		
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new UserMap());
			modelBuilder.Configurations.Add(new RoleMap());
			modelBuilder.Configurations.Add(new MessageMap());

			base.OnModelCreating(modelBuilder);
		}
	}
}
