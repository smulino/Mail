using System;

namespace Mail.Data.Entities
{
	public class Message
	{
		public int Id { get; set; }

		public string Text { get; set; }

		public int FromUserId { get; set; }

		public int ToUserId { get; set; }

		public DateTime CreationTimeUTC { get; set; }
	}
}
