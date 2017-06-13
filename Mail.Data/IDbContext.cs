using System.Data.Entity;

namespace Mail.Data
{
	public interface IDbContext
	{
		IDbSet<TEntity> Set<TEntity>() where TEntity : class;
	}
}
