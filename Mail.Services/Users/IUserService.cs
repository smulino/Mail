using Mail.Data.Entities;

namespace Mail.Services.Users
{
	public interface IUserService
	{
		User GetUserById(int id);
	}
}
