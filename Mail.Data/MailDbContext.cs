using Mail.Data.Mapping;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

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

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.Add(new UserMap());
			modelBuilder.Configurations.Add(new RoleMap());
			modelBuilder.Configurations.Add(new MessageMap());

			base.OnModelCreating(modelBuilder);
		}
	}
}
