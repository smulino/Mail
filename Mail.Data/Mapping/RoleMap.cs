using Mail.Data.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Mail.Data.Mapping
{
	public class RoleMap : EntityTypeConfiguration<Role>
	{
		public RoleMap()
		{
			this.ToTable("Roles");

			this.HasKey(r => r.Id);
		}
	}
}
