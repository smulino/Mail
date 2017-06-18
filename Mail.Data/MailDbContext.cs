using Mail.Data.Entities;
using Mail.Data.Mapping;
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
