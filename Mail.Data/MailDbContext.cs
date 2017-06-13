using System.Data.Entity;

namespace Mail.Data
{
	public class MailDbContext : DbContext, IDbContext
	{
		public MailDbContext()
			: base("Default")
		{
			
		}
	}
}
