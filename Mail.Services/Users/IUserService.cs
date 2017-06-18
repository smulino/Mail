using Mail.Data.Domain;

namespace Mail.Services.Users
{
	public interface IUserService
	{
		User GetUserById(int id);
	}
}
