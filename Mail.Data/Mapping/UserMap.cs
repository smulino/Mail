using Mail.Data.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Mail.Data.Mapping
{
	public class UserMap : EntityTypeConfiguration<User>
	{
		public UserMap()
		{
			this.ToTable("Users");

			this.HasKey(u => u.Id);
		}
	}
}
