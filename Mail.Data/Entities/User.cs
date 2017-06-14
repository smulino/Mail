using System.Collections.Generic;

namespace Mail.Data.Entities
{
	public class User
	{
		public User()
		{
			Roles = new List<Role>();
		}

		public int Id { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string UserName { get; set; }

		public string Password { get; set; }

		public virtual ICollection<Role> Roles { get; set; }
	}
}
