using Mail.Data.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Mail.Data.Mapping
{
	public class UserMap : EntityTypeConfiguration<User>
	{
		public UserMap()
		{
			this.ToTable("Users");

			this.HasKey(u => u.Id);

			this.HasMany(u => u.Roles)
				.WithMany()
				.Map(ur =>
					{
						ur.MapLeftKey("UserId");
						ur.MapRightKey("RoleId");
						ur.ToTable("UserRoles");
					}
				);
		}
	}
}
